using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelText;
    private string baseScoreText;
    private string baseTimeText;
    private string baseLevelText;

    void Start() {
        baseScoreText = scoreText.text;
        baseTimeText = timeText.text;
        baseLevelText = levelText.text;
    }

    void Update() {
        UpdateScoreUI();
        UpdateTimeUI();
        UpdateLevelUI();
    }

    private void UpdateLevelUI()
    {
        levelText.text = baseLevelText + Stats.Level.ToString();
    }

    private void UpdateTimeUI()
    {
        timeText.text = baseTimeText + Stats.TimeToPlay.ToString();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = baseScoreText + Stats.Points.ToString();
    }
}
