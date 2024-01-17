using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class Stats : MonoBehaviour
{
    internal static int Points {get; private set;} = 0;
    internal static float TotalTimePassed {get; private set;} = 0f;
    internal static int Level {get; private set;} = 1;

    void Update()
    {
        TimerUpdate();
        LevelUpdate();
    }

    private void TimerUpdate()
    {
        TotalTimePassed += Time.deltaTime;
    }

    void ResetTimer() {
        TotalTimePassed = 0f;
    }

    private void LevelUpdate()
    {
        Level = (int)TotalTimePassed / 10;
        if (Level < 1) Level = 1;
    }

    internal static void AddPoint() {
        Points += 10;
    }

    internal static void RemovePoint() {
        if (Points >= 5) {
            Points -= 5;
        }
        else {
            Points = 0;
        }
    }
}
