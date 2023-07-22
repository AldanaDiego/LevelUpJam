using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        SFXAudioManager audioManager = SFXAudioManager.GetInstance();
        _replayButton.onClick.AddListener(OnReplayButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
        _replayButton.onClick.AddListener(audioManager.OnButtonClicked);
        _quitButton.onClick.AddListener(audioManager.OnButtonClicked);
    }

    private void OnReplayButtonClicked()
    {
        _menuContainer.SetActive(false);
        OnGameRestart?.Invoke(this, EventArgs.Empty);
    }

    private void OnQuitButtonClicked()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private void OnGlobalTimerEnded(object sender, EventArgs empty)
    {
        _menuContainer.SetActive(true);
    }
}
