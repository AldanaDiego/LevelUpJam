using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    private const float SPAWN_COOLDOWN = 5f;
    private Vector3 SPAWN_OFFSET = new Vector3(0f, 0f, 5f);

    [SerializeField] private List<GoalTable> _goalTables;
    [SerializeField] private Customer _customerPrefab;

    private float _cooldownTimer;
    private List<int> _availablePositions;

    private void Start()
    {
        _cooldownTimer = 0f;
        _availablePositions = new List<int>();
        for (int i = 0; i < _goalTables.Count; i++)
        {
            _availablePositions.Add(i);
        }
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer >= SPAWN_COOLDOWN)
        {
            _cooldownTimer -= SPAWN_COOLDOWN;
            SpawnCustomer();
        }
    }

    private void SpawnCustomer()
    {
        if (_availablePositions.Count > 0)
        {
            int position = _availablePositions[Random.Range(0, _availablePositions.Count)];
            _availablePositions.Remove(position);
            Vector3 spawnPosition = _goalTables[position].transform.position + SPAWN_OFFSET;
            Customer customer = Instantiate(_customerPrefab, spawnPosition, Quaternion.Euler(0f, 180f, 0f));
            customer.OnCustomerGone += OnCustomerGone;
            customer.Setup(position, _goalTables[position]);
        }
    }

    private void OnCustomerGone(object sender, int position)
    {
        Customer customer = (Customer) sender;
        customer.OnCustomerGone -= OnCustomerGone;
        _availablePositions.Add(position);
        Destroy(customer.gameObject);
    }
}
