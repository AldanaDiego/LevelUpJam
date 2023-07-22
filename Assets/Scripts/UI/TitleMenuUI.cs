using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private Button _tutorialCloseButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _settingsCloseButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private Image _defaultBackground;
    [SerializeField] private Image _clearBackground;
    [SerializeField] private TextMeshProUGUI _titleText;

    [SerializeField] private GameObject _buttonMenuContainer;
    [SerializeField] private GameObject _tutorialMenuContainer;
    [SerializeField] private GameObject _settingsMenuContainer;

    private TutorialMenuUI _tutorialMenuUI;
    private SettingsMenuUI _settingsMenuUI;

    private void Start()
    {
        SFXAudioManager audioManager = SFXAudioManager.GetInstance();
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _tutorialButton.onClick.AddListener(OnTutorialButtonClicked);
        _tutorialCloseButton.onClick.AddListener(OnTutorialCloseButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _settingsCloseButton.onClick.AddListener(OnSettingsCloseButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);

        _startButton.onClick.AddListener(audioManager.OnButtonClicked);
        _tutorialButton.onClick.AddListener(audioManager.OnButtonClicked);
        _tutorialCloseButton.onClick.AddListener(audioManager.OnButtonClicked);
        _settingsButton.onClick.AddListener(audioManager.OnButtonClicked);
        _settingsCloseButton.onClick.AddListener(audioManager.OnButtonClicked);
        _exitButton.onClick.AddListener(audioManager.OnButtonClicked);

        _tutorialMenuContainer.SetActive(false);
        _settingsMenuContainer.SetActive(false);
        _tutorialMenuUI = GetComponent<TutorialMenuUI>();
        _settingsMenuUI = GetComponent<SettingsMenuUI>();
        ShowDefaultBackground();
    }

    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnTutorialButtonClicked()
    {
        ShowClearBackground();
        _buttonMenuContainer.SetActive(false);
        _tutorialMenuContainer.SetActive(true);
        _tutorialMenuUI.OnShow();
    }

    private void OnTutorialCloseButtonClicked()
    {
        ShowDefaultBackground();
        _tutorialMenuContainer.SetActive(false);
        _buttonMenuContainer.SetActive(true);
    }

    private void OnSettingsButtonClicked()
    {
        ShowClearBackground();
        _buttonMenuContainer.SetActive(false);
        _settingsMenuContainer.SetActive(true);
        _settingsMenuUI.OnShow();
    }

    private void OnSettingsCloseButtonClicked()
    {
        ShowDefaultBackground();
        _settingsMenuContainer.SetActive(false);
        _buttonMenuContainer.SetActive(true);
    }

    private void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void ShowDefaultBackground()
    {
        _defaultBackground.gameObject.SetActive(true);
        _titleText.gameObject.SetActive(true);
        _clearBackground.gameObject.SetActive(false);
    }

    private void ShowClearBackground()
    {
        _clearBackground.gameObject.SetActive(true);
        _defaultBackground.gameObject.SetActive(false);
        _titleText.gameObject.SetActive(false);
    }
}
