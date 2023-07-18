using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreSystem _scoreSystem;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        _scoreSystem.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(object sender, int newScore)
    {
        _scoreText.text = $"Score: {newScore}";
    }
}
