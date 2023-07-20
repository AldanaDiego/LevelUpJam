using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomerSpawner : MonoBehaviour
{
    private const float SPAWN_COOLDOWN = 5f;
    private const float COOLDOWN_START_VALUE = 2f;
    private Vector3 SPAWN_OFFSET = new Vector3(0f, 0f, 5f);

    [SerializeField] private List<GoalTable> _goalTables;
    [SerializeField] private Customer _customerPrefab;

    private float _cooldownTimer;
    private List<int> _availablePositions;
    private List<Customer> _spawnedCustomers;
    private bool _isActive;

    private void Start()
    {
        _isActive = false;
        _cooldownTimer = COOLDOWN_START_VALUE;
        _availablePositions = new List<int>();
        _spawnedCustomers = new List<Customer>();
        for (int i = 0; i < _goalTables.Count; i++)
        {
            _availablePositions.Add(i);
        }
        GameTimer timer = GameTimer.GetInstance();
        timer.OnGlobalTimerStarted += OnGlobalTimerStarted;
        timer.OnGlobalTimerEnded += OnGlobalTimerEnded;
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
                SpawnCustomer();
            }
        }
    }

    private void SpawnCustomer()
    {
        if (_availablePositions.Count > 0)
        {
            int position = _availablePositions[UnityEngine.Random.Range(0, _availablePositions.Count)];
            _availablePositions.Remove(position);
            Vector3 spawnPosition = _goalTables[position].transform.position + SPAWN_OFFSET;
            Customer customer = Instantiate(_customerPrefab, spawnPosition, Quaternion.Euler(0f, 180f, 0f));
            customer.OnCustomerGone += OnCustomerGone;
            customer.Setup(position, _goalTables[position]);
            _spawnedCustomers.Add(customer);
        }
    }

    private void OnCustomerGone(object sender, int position)
    {
        Customer customer = (Customer) sender;
        customer.OnCustomerGone -= OnCustomerGone;
        _availablePositions.Add(position);
        _spawnedCustomers.Remove(customer);
        Destroy(customer.gameObject);
    }

    private void OnGlobalTimerStarted(object sender, EventArgs empty)
    {
        _isActive = true;
    }

    private void OnGlobalTimerEnded(object sender, EventArgs empty)
    {
        _isActive = false;
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        _availablePositions.Clear();
        for (int i = 0; i < _goalTables.Count; i++)
        {
            _availablePositions.Add(i);
        }
        foreach (Customer customer in _spawnedCustomers)
        {
            Destroy(customer.gameObject);
        }
        _spawnedCustomers.Clear();
        _cooldownTimer = COOLDOWN_START_VALUE;
    }

    private void OnDestroy()
    {
        GameEndMenuUI.OnGameRestart -= OnGameRestart;    
    }
}
