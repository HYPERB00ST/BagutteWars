using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI ammoText;
    private string baseScoreText;
    private string baseTimeText;
    private string baseLevelText;
    private string baseAmmoText;

    void Start() {
        baseScoreText = scoreText.text;
        baseTimeText = timeText.text;
        baseLevelText = levelText.text;
        baseAmmoText = ammoText.text;
    }

    void Update() {
        UpdateScoreUI();
        UpdateTimeUI();
        UpdateLevelUI();
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = baseAmmoText + combat.GetCurrentAmmo().ToString();
    }

    private void UpdateLevelUI()
    {
        levelText.text = baseLevelText + Stats.Level.ToString();
    }

    private void UpdateTimeUI()
    {
        timeText.text = baseTimeText + Stats.PlayTime.ToString();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = baseScoreText + Stats.Points.ToString();
    }
}
