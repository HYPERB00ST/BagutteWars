using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    internal float TimePassed {get; private set;}
    void Update()
    {
        TimePassed += Time.deltaTime;
    }
}