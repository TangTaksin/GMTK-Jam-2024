using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playTime;
    // Start is called before the first frame update
    void Start()
    {
        TimeSystem.OnPause.Invoke();
        TimeSystem.SentTime += GetTime;

    }


    public void GetTime(float time)
    {
        var _time = TimeSpan.FromSeconds(time);
        playTime.text = string.Format("{0:00}:{1:00}:{2:000}", _time.Minutes, _time.Seconds, _time.Milliseconds);
        print(time);

    }

    public void BackToMenu()
    {
        TimeSystem.OnReset.Invoke();
        TimeSystem.OnPause.Invoke();
        
        SceneManager.LoadScene("MainMenu");
    }
}
