using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    Animator _animator;
    Camera _camera;

    public Transform _shape;
    Transform _player;

    public delegate void FadeEvent();
    public static FadeEvent OnFadeIn;
    public static FadeEvent OnFadeOut;

    public delegate void FinishEvent(string _anim);
    public static FinishEvent FadeFinished;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _camera = Camera.main;

        SceneManager.sceneLoaded += OnSceneLoad;
        OnFadeIn += PlayFadeIn;
        OnFadeOut += PlayFadeOut;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
        OnFadeIn -= PlayFadeIn;
        OnFadeOut -= PlayFadeOut;
    }

    public void PlayFadeIn()
    {
        UpdateShapePosition();
        _animator.Play("Fade_In");
        
        _player = null;
    }

    public void PlayFadeOut()
    {
        UpdateShapePosition();
        _animator.Play("Fade_Out");
    }

    public void FinishPlaying(string _animName)
    {
        FadeFinished?.Invoke(_animName);
    }

    void OnSceneLoad(Scene _scene, LoadSceneMode _lsm)
    {
        if (GameObject.Find("Player_Main"))
            _player = GameObject.Find("Player_Main").transform;

        PlayFadeOut();
    }

    void UpdateShapePosition()
    {
        if (_player)
            _shape.position = _camera.WorldToScreenPoint(_player.position);
    }
}
