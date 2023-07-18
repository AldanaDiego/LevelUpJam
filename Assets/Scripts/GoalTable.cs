using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoalTable : MonoBehaviour
{
    public event EventHandler<int> OnFoodBlockReceived;

    private bool _canReceiveFood;

    private void Start()
    {
        _canReceiveFood = false;
    }

    public bool CanReceiveFood()
    {
        return _canReceiveFood;
    }

    public void SetCanReceiveFood(bool canReceive)
    {
        _canReceiveFood = canReceive;
    }

    public void Receive(int amount)
    {
        OnFoodBlockReceived?.Invoke(this, amount);
    }
}
