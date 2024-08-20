using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    static float totalPlayTime;
    static bool timeIsRunning;

    public TextMeshProUGUI clock_txt;

    public delegate void TimerDelegate();
    public static TimerDelegate OnStart;
    public static TimerDelegate OnPause;
    public static TimerDelegate OnReset;

    public delegate void TimerData(float _time);
    public static TimerData SentTime;

    private static GameObject sampleInstance;

    private void Awake()
    {
        if (sampleInstance != null)
            Destroy(sampleInstance);

        sampleInstance = gameObject;
        DontDestroyOnLoad(this);

    }

    private void OnEnable()
    {
        OnStart += StartTimer;
        OnPause += PauseTimer;
        OnReset += ResetTimer;

        UpdateClock(totalPlayTime);
    }

    private void OnDisable()
    {
        OnStart -= StartTimer;
        OnPause -= PauseTimer;
        OnReset -= ResetTimer;
    }

    private void Update()
    {
        if (timeIsRunning)
        {
            totalPlayTime += Time.deltaTime;
            UpdateClock(totalPlayTime);

        }
    }

    void UpdateClock(float _sample)
    {
        var _time = TimeSpan.FromSeconds(_sample);
        clock_txt.text = string.Format("{0:00}:{1:00}:{2:000}", _time.Minutes, _time.Seconds, _time.Milliseconds);
    }

    public void StartTimer()
    {
        timeIsRunning = true;
    }

    public void PauseTimer()
    {
        timeIsRunning = false;
        SentTime?.Invoke(totalPlayTime);
    }

    public void ResetTimer()
    {
        totalPlayTime = 0;
    }
}
