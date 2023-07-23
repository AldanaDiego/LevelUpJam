using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreSystem : MonoBehaviour
{
    public event EventHandler<ScoreChangedParams> OnScoreChanged;
    private int _score;

    private void Start()
    {
        _score = 0;
        Customer.OnCustomerServed += OnCustomerServed;
        Customer.OnCustomerSuccess += OnCustomerSuccess;
        Customer.OnCustomerFail += OnCustomerFail;
        GameEndMenuUI.OnGameRestart += OnGameRestart;
        FoodSpawner.OnFoodBlockDiscarded += OnFoodBlockDiscarded;
    }

    private void AddScore(int amount)
    {
        _score += amount;
        OnScoreChanged?.Invoke(this, new ScoreChangedParams(){Score = _score, ScoreDiff = amount});
    }

    private void SubstractScore(int amount)
    {
        int scoreDiff = (_score >= amount) ? amount * -1 : _score * -1;
        _score = Math.Max(0, _score - amount);
        OnScoreChanged?.Invoke(this, new ScoreChangedParams(){Score = _score, ScoreDiff = scoreDiff});
    }

    private void OnCustomerServed(object sender, EventArgs empty)
    {
        AddScore(1);
    }

    private void OnCustomerSuccess(object sender, EventArgs empty)
    {
        AddScore(5);
    }

    private void OnCustomerFail(object sender, EventArgs empty)
    {
        SubstractScore(3);
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        _score = 0;
        OnScoreChanged?.Invoke(this, new ScoreChangedParams(){Score = _score, ScoreDiff = 0});
    }

    private void OnFoodBlockDiscarded(object sender, EventArgs empty)
    {
        SubstractScore(1);
    }

    private void OnDestroy()
    {
        Customer.OnCustomerServed -= OnCustomerServed;
        Customer.OnCustomerSuccess -= OnCustomerSuccess;
        Customer.OnCustomerFail -= OnCustomerFail;
        GameEndMenuUI.OnGameRestart -= OnGameRestart;
        FoodSpawner.OnFoodBlockDiscarded -= OnFoodBlockDiscarded;
    }

    public class ScoreChangedParams: EventArgs
    {
        public int Score;
        public int ScoreDiff;
    }
}
