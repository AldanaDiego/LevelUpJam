using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreSystem : MonoBehaviour
{
    public event EventHandler<int> OnScoreChanged;
    private int _score;

    private void Start()
    {
        _score = 0;
        Customer.OnCustomerServed += OnCustomerServed;
        Customer.OnCustomerSuccess += OnCustomerSuccess;
        Customer.OnCustomerFail += OnCustomerFail;
        GameEndMenuUI.OnGameRestart += OnGameRestart;
    }

    private void OnCustomerServed(object sender, EventArgs empty)
    {
        _score += 1;
        OnScoreChanged?.Invoke(this, _score);
    }

    private void OnCustomerSuccess(object sender, EventArgs empty)
    {
        _score += 5;
        OnScoreChanged?.Invoke(this, _score);
    }

    private void OnCustomerFail(object sender, EventArgs empty)
    {
        _score = Math.Max(0, _score - 3);
        OnScoreChanged?.Invoke(this, _score);
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        _score = 0;
        OnScoreChanged?.Invoke(this, _score);
    }

    private void OnDestroy()
    {
        Customer.OnCustomerServed -= OnCustomerServed;
        Customer.OnCustomerSuccess -= OnCustomerSuccess;
        Customer.OnCustomerFail -= OnCustomerFail;
        GameEndMenuUI.OnGameRestart -= OnGameRestart;
    }
}
