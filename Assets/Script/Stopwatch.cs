using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    public float timer;
    public bool startTime;

    public void Awake()
    {
        timer = 0;
        startTime = false;
    }
    public void FixedUpdate()
    {
        AddTime();
    }

    public void AddTime() 
    {
        timer += startTime ? Time.deltaTime : 0;
    }

    public void ResetTime()
    {
        timer = 0;
    }

    public void StartTimer()
    {
        ResetTime();
        startTime = true;
    }

    public void StopTimer() 
    {
        startTime = false;
    }
}
