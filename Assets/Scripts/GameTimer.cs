using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTimer : Singleton<GameTimer>
{
    public event EventHandler OnGlobalTimerStarted;
    public event EventHandler OnGlobalTimerEnded;

    private float _globalTimer;
    private float _startTimer;
    private bool _isGlobalTimerRunning;

    protected override void Awake()
    {
        base.Awake();
        _globalTimer = 30f;
        _startTimer = 4f;
        _isGlobalTimerRunning = false;
    }

    private void Start()
    {
        GameEndMenuUI.OnGameRestart += OnGameRestart;
    }

    private void Update()
    {
        if (_startTimer > 0 && !_isGlobalTimerRunning)
        {
            _startTimer -= Time.deltaTime;
            if (_startTimer <= 0f)
            {
                _isGlobalTimerRunning = true;
                OnGlobalTimerStarted?.Invoke(this, EventArgs.Empty);
            }
        }
        else if (_isGlobalTimerRunning)
        {
            _globalTimer -= Time.deltaTime;
            if (_globalTimer <= 0f)
            {
                _isGlobalTimerRunning = false;
                OnGlobalTimerEnded?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public float GetStartTimer()
    {
        return _startTimer;
    }

    public float GetGlobalTimer()
    {
        return _globalTimer;
    }

    public bool IsGlobalTimeRunning()
    {
        return _isGlobalTimerRunning;
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        _isGlobalTimerRunning = false;
        _globalTimer = 30f;
        _startTimer = 4f;
    }
}
