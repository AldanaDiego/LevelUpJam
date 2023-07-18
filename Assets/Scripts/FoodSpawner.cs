using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodSpawner : MonoBehaviour
{
    private const float SPAWN_COOLDOWN = 3f;
    public const int START_FOOD_BLOCK_POS = -5;
    public const int LAST_FOOD_BLOCK_POS = 5;

    [SerializeField] private FoodBlock _foodPrefab;
    [SerializeField] private Vector3 _spawnPosition;
    private List<FoodBlock> _foodBlocks;
    private float _cooldownTimer;
    private bool _isActive;

    private void Start()
    {
        _isActive = false;
        _foodBlocks = new List<FoodBlock>();
        _cooldownTimer = 0f;
        FoodBlock.OnFoodBlockGrabbed += OnFoodBlockGrabbed;
        GameTimer timer = GameTimer.GetInstance();
        timer.OnGlobalTimerStarted += OnGlobalTimerStarted;
        timer.OnGlobalTimerEnded += OnGlobalTimerEnded;
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
        FoodBlock foodBlock = Instantiate(_foodPrefab, _spawnPosition, Quaternion.identity);
        _foodBlocks.Add(foodBlock);
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
    }
}
