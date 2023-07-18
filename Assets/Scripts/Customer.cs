using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Customer : MonoBehaviour
{
    public event EventHandler<int> OnCustomerGone;
    public static event EventHandler<int> OnCustomerServed;

    private Transform _transform;
    private GoalTable _goalTable;
    private int _position;
    private bool _isMovingIn;
    private int _foodNeeded;

    public void Setup(int position, GoalTable goalTable)
    {
        _transform = transform;
        _foodNeeded = 1; //TODO should be random
        _position = position;
        _goalTable = goalTable;
        _goalTable.OnFoodBlockReceived += OnFoodBlockReceived;
        StartCoroutine(MoveIn());
    }

    private IEnumerator MoveIn()
    {
        _isMovingIn = true;
        float moveInSpeed = -5f;
        while (_isMovingIn)
        {
            _transform.position = new Vector3(
                _transform.position.x,
                _transform.position.y,
                _transform.position.z + (moveInSpeed * Time.deltaTime)
            );
            yield return new WaitForFixedUpdate();
        }
        _goalTable.SetCanReceiveFood(true);
    }

    private IEnumerator MoveOut()
    {
        _goalTable.SetCanReceiveFood(false);
        float moveOutSpeed = 5f;
        while (_transform.position.z < 15f)
        {
            _transform.position = new Vector3(
                _transform.position.x,
                _transform.position.y,
                _transform.position.z + (moveOutSpeed * Time.deltaTime)
            );
            yield return new WaitForFixedUpdate();
        }
        OnCustomerGone?.Invoke(this, _position);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _isMovingIn = false;
    }

    private void OnDestroy()
    {
        _goalTable.OnFoodBlockReceived -= OnFoodBlockReceived;
    }

    private void OnFoodBlockReceived(object sender, int foodAmount)
    {
        _foodNeeded -= foodAmount;
        if (_foodNeeded <= 0)
        {
            int score = (_foodNeeded == 0) ? 3 : _foodNeeded;
            OnCustomerServed?.Invoke(this, score);
            StartCoroutine(MoveOut());
        }
    }
}