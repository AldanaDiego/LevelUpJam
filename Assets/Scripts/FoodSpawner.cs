using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private const float SPAWN_COOLDOWN = 3f;
    public const int START_FOOD_BLOCK_POS = -5;
    public const int LAST_FOOD_BLOCK_POS = 5;

    [SerializeField] private FoodBlock _foodPrefab;
    [SerializeField] private Vector3 _spawnPosition;
    private List<FoodBlock> _foodBlocks;
    private float _cooldownTimer;

    private void Start()
    {
        _foodBlocks = new List<FoodBlock>();
        _cooldownTimer = 0f;
    }

    private void Update()
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

    private void SpawnFoodBlock()
    {
        FoodBlock foodBlock = Instantiate(_foodPrefab, _spawnPosition, Quaternion.identity);
        _foodBlocks.Add(foodBlock);
    }

    private bool CanSpawn()
    {
        return !Physics.Raycast(_spawnPosition, Vector3.left, _spawnPosition.x - LAST_FOOD_BLOCK_POS);
    }
}
