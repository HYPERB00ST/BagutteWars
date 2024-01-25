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
    [SerializeField] private GameObject ammoParent;
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
        UpdateAmmoUI();
        CheckReloadUI();
    }

    private void CheckReloadUI()
    {
        if (combat.GetReloadStatus()) {
            for (int i = 0; i < 3; i++) { // Hacking solution, I know
                ammoParent.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    private void UpdateAmmoUI()
    {
        for (int i = 2; i > combat.GetCurrentAmmo() - 1; i--) {
            ammoParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void UpdateLevelUI()
    {
        levelText.text = baseLevelText + " " + Stats.Level.ToString();
    }

    private void UpdateTimeUI()
    {
        timeText.text = baseTimeText + " " + Stats.PlayTime.ToString();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = baseScoreText + " " + Stats.Points.ToString();
    }
}
