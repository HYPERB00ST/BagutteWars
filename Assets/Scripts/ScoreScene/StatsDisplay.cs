using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI level;
    void Start() {
        score.text += Stats.Points;
        time.text += Stats.TotalTimePassed;
        level.text += Stats.Level;
    }
}
