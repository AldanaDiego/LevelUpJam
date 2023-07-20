using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GlobalTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startCountdownText;
    [SerializeField] private TextMeshProUGUI _globalTimerText;

    private GameTimer _gameTimer;
    private bool _isGlobalTimerRunning;
    private bool _hasTimerStarted;

    private void Start()
    {
        _gameTimer = GameTimer.GetInstance();
        _startCountdownText.text = "";
        _isGlobalTimerRunning = false;
        _hasTimerStarted = false;
        UpdateGlobalTimeText();
        _gameTimer.OnGlobalTimerStarted += OnGlobalTimerStarted;
        _gameTimer.OnGlobalTimerEnded += OnGlobalTimerEnded;
        GameEndMenuUI.OnGameRestart += OnGameRestart;
    }

    private void Update()
    {
        if (_isGlobalTimerRunning)
        {
            UpdateGlobalTimeText();
        }
        else if (!_hasTimerStarted)
        {
            int startTime = Mathf.CeilToInt(_gameTimer.GetStartTimer());
            if (startTime < 4)
            {
                _startCountdownText.text = startTime.ToString();
            }
        }
    }

    private void UpdateGlobalTimeText()
    {
        float timeLeft = _gameTimer.GetGlobalTimer();
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        _globalTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnGlobalTimerStarted(object sender, EventArgs empty)
    {
        _isGlobalTimerRunning = true;
        _hasTimerStarted = true;
        _startCountdownText.text = "";
    }

    private void OnGlobalTimerEnded(object sender, EventArgs empty)
    {
        _isGlobalTimerRunning = false;
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        _isGlobalTimerRunning = false;
        _hasTimerStarted = false;
    }

    private void OnDestroy()
    {
        GameEndMenuUI.OnGameRestart -= OnGameRestart;    
    }
}
