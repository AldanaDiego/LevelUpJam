using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameEndMenuUI : MonoBehaviour
{
    public static event EventHandler OnGameRestart;

    [SerializeField] private GameObject _menuContainer;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _menuContainer.SetActive(false);
        GameTimer gameTimer = GameTimer.GetInstance();
        gameTimer.OnGlobalTimerEnded += OnGlobalTimerEnded;
        _replayButton.onClick.AddListener(OnReplayButtonClicked);
    }

    public void OnReplayButtonClicked()
    {
        _menuContainer.SetActive(false);
        OnGameRestart?.Invoke(this, EventArgs.Empty);
    }

    private void OnGlobalTimerEnded(object sender, EventArgs empty)
    {
        _menuContainer.SetActive(true);
    }
}
