using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTimer : Singleton<GameTimer>
{
    public event EventHandler OnGlobalTimerStarted;
    public event EventHandler OnGlobalTimerEnded;
    public event EventHandler OnTenSecondsLeft;
    public event EventHandler OnFiveSecondsLeft;

    private float _globalTimer;
    private float _startTimer;
    private bool _isGlobalTimerRunning;
    private float _stageTimeAmount = 60f;
    private bool _hasTriggeredTenSecondsLeft;
    private bool _hasTriggeredFiveSecondsLeft;

    protected override void Awake()
    {
        base.Awake();
        _globalTimer = _stageTimeAmount;
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
                _hasTriggeredFiveSecondsLeft = false;
                _hasTriggeredTenSecondsLeft = false;
                OnGlobalTimerStarted?.Invoke(this, EventArgs.Empty);
            }
        }
        else if (_isGlobalTimerRunning)
        {
            _globalTimer -= Time.deltaTime;
            if (!_hasTriggeredTenSecondsLeft && _globalTimer <= 10f)
            {
                _hasTriggeredTenSecondsLeft = true;
                OnTenSecondsLeft?.Invoke(this, EventArgs.Empty);
            }
            if (!_hasTriggeredFiveSecondsLeft && _globalTimer <= 7.5f)
            {
                _hasTriggeredFiveSecondsLeft = true;
                OnFiveSecondsLeft?.Invoke(this, EventArgs.Empty);
            }
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
        _globalTimer = _stageTimeAmount;
        _startTimer = 4f;
    }

    private void OnDestroy()
    {
        GameEndMenuUI.OnGameRestart -= OnGameRestart;    
    }
}
