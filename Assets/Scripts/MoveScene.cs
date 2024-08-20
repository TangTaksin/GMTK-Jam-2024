using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public string scene_Name;

    private void OnEnable()
    {
        Fader.FadeFinished += SceneLoad;
    }

    public void StartFade()
    {
        Fader.OnFadeIn?.Invoke();
    }

    void SceneLoad(string _anim)
    {
        if (_anim == "Fade_In")
        {
            SceneManager.LoadScene(scene_Name);
            Fader.OnFadeIn?.Invoke();
        }
    }
}
