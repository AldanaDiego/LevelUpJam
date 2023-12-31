using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodSpawner : MonoBehaviour
{
    public static event EventHandler OnFoodBlockSpawned;
    public static event EventHandler OnFoodBlockDiscarded;
    public static event EventHandler OnDiscardEnded;

    private const float SPAWN_COOLDOWN = 2.5f;
    private const float COOLDOWN_START_VALUE = 2f;
    public const int START_FOOD_BLOCK_POS = -5;
    public const int LAST_FOOD_BLOCK_POS = 5;

    [SerializeField] private FoodBlock[] _foodPrefabs;
    [SerializeField] private Vector3 _spawnPosition;
    private List<FoodBlock> _foodBlocks;
    private float _cooldownTimer;
    private bool _isActive;

    private void Start()
    {
        _isActive = false;
        _foodBlocks = new List<FoodBlock>();
        _cooldownTimer = COOLDOWN_START_VALUE;
        FoodBlock.OnFoodBlockGrabbed += OnFoodBlockGrabbed;
        GameTimer timer = GameTimer.GetInstance();
        timer.OnGlobalTimerStarted += OnGlobalTimerStarted;
        timer.OnGlobalTimerEnded += OnGlobalTimerEnded;
        timer.OnFiveSecondsLeft += OnFiveSecondsLeft;
        GameEndMenuUI.OnGameRestart += OnGameRestart;
    }

    private void Update()
    {
        if (_isActive)
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= SPAWN_COOLDOWN)
            {
                _cooldownTimer -= SPAWN_COOLDOWN;
                if (CanSpawn())
                {
                    SpawnFoodBlock();
                }
            }
        }
    }

    private void SpawnFoodBlock()
    {
        FoodBlock prefab = _foodPrefabs[UnityEngine.Random.Range(0, _foodPrefabs.Length)];
        FoodBlock foodBlock = Instantiate(prefab, _spawnPosition, Quaternion.identity);
        _foodBlocks.Add(foodBlock);
        OnFoodBlockSpawned?.Invoke(this, EventArgs.Empty);
    }

    private bool CanSpawn()
    {
        return !Physics.Raycast(_spawnPosition, Vector3.left, _spawnPosition.x - LAST_FOOD_BLOCK_POS);
    }

    private IEnumerator SquashBlocksToLeft(FoodBlock grabbed)
    {
        int pos = _foodBlocks.IndexOf(grabbed);
        _foodBlocks.RemoveAt(pos);
        for (int i = pos; i < _foodBlocks.Count; i++)
        {
            _foodBlocks[i].Move();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator DiscardRemainingFood()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (FoodBlock foodBlock in _foodBlocks)
        {
            Destroy(foodBlock.gameObject);
            OnFoodBlockDiscarded?.Invoke(this, EventArgs.Empty);
            yield return new WaitForSeconds(1f);
        }
        _foodBlocks.Clear();
        OnDiscardEnded?.Invoke(this, EventArgs.Empty);
    }

    private void OnFoodBlockGrabbed(object sender, EventArgs empty)
    {
        StartCoroutine(SquashBlocksToLeft((FoodBlock) sender));
    }

    private void OnGlobalTimerStarted(object sender, EventArgs empty)
    {
        _isActive = true;
    }

    private void OnGlobalTimerEnded(object sender, EventArgs empty)
    {
        _isActive = false;
        StartCoroutine(DiscardRemainingFood());
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        
        _cooldownTimer = COOLDOWN_START_VALUE;
    }

    private void OnFiveSecondsLeft(object sender, EventArgs empty)
    {
        _isActive = false;
    }

    private void OnDestroy()
    {
        FoodBlock.OnFoodBlockGrabbed -= OnFoodBlockGrabbed;
        GameEndMenuUI.OnGameRestart -= OnGameRestart;
    }
}
