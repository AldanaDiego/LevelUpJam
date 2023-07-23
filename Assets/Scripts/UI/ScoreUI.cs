using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreSystem _scoreSystem;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _scoreDiffText;

    private void Start()
    {
        _scoreSystem.OnScoreChanged += OnScoreChanged;
    }

    private IEnumerator ShowScoreDiff(int scoreDiff)
    {
        _scoreDiffText.color = (scoreDiff > 0) ? Color.green : Color.red;
        string sign = (scoreDiff > 0) ? "+" : "";
        _scoreDiffText.text = $"{sign}{scoreDiff}";
        yield return new WaitForSeconds(0.5f);
        _scoreDiffText.text = "";
    }

    private void OnScoreChanged(object sender, ScoreSystem.ScoreChangedParams scoreChange)
    {
        _scoreText.text = $"{scoreChange.Score}";
        if (scoreChange.ScoreDiff != 0)
        {
            StartCoroutine(ShowScoreDiff(scoreChange.ScoreDiff));
        }
    }
}
