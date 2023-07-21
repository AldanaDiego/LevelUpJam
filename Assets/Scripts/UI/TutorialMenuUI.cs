using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialMenuUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tutorialPanels;
    [SerializeField] private TextMeshProUGUI _pageText;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    private int _currentTutorial;

    private void Start()
    {
        _leftButton.onClick.AddListener(OnLeftButtonClicked);
        _rightButton.onClick.AddListener(OnRightButtonClicked);
    }

    public void OnShow()
    {
        _currentTutorial = 0;
        UpdateVisiblePanel();
        UpdatePageText();
        UpdateButtons();
    }

    private void UpdateVisiblePanel()
    {
        foreach (GameObject panel in _tutorialPanels)
        {
            panel.SetActive(false);
        }
        _tutorialPanels[_currentTutorial].SetActive(true);
    }

    private void OnLeftButtonClicked()
    {
        if (_currentTutorial - 1 >= 0)
        {
            _currentTutorial--;
            UpdateVisiblePanel();
            UpdatePageText();
            UpdateButtons();
        }
    }

    private void OnRightButtonClicked()
    {
        if (_currentTutorial + 1 < _tutorialPanels.Count)
        {
            _currentTutorial++;
            UpdateVisiblePanel();
            UpdatePageText();
            UpdateButtons();
        }
    }

    private void UpdatePageText()
    {
        _pageText.text = $"{_currentTutorial+1}/{_tutorialPanels.Count}";
    }

    private void UpdateButtons()
    {
        if (_currentTutorial == 0)
        {
            _leftButton.enabled = false;
            _rightButton.enabled = true;
        }
        else if (_currentTutorial + 1 == _tutorialPanels.Count)
        {
            _leftButton.enabled = true;
            _rightButton.enabled = false;
        }
        else
        {
            _leftButton.enabled = true;
            _rightButton.enabled = true;
        }
    }
}
