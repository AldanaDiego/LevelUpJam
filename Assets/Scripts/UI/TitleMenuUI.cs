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
    [SerializeField] private Button _exitButton;
    [SerializeField] private Image _defaultBackground;
    [SerializeField] private Image _clearBackground;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private GameObject _buttonMenuContainer;
    [SerializeField] private GameObject _tutorialMenuContainer;

    private TutorialMenuUI _tutorialMenuUI;

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _tutorialButton.onClick.AddListener(OnTutorialButtonClicked);
        _tutorialCloseButton.onClick.AddListener(OnTutorialCloseButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        _tutorialMenuContainer.SetActive(false);
        _tutorialMenuUI = GetComponent<TutorialMenuUI>();
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
