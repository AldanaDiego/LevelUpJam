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
    }

    private void OnCustomerServed(object sender, int score)
    {
        _score = Math.Max(0, _score + score);
        OnScoreChanged?.Invoke(this, _score);
    }
}
