using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    internal static float TotalTimePassed {get; private set;}
    void Update()
    {
        TotalTimePassed += Time.deltaTime;
    }

    void ResetTimer() {
        TotalTimePassed = 0f;
    }
}