using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip music_Bg;
    public AudioClip[] scaleUp_sfx;
    public AudioClip scaleDown_sfx;
    public AudioClip switchMode_sfx;
    public AudioClip selectPast_sfx;
    public AudioClip selectside_sfx;
    public AudioClip resetScale_sfx;
    public AudioClip[] bound_sfx;



    [Header("Settings")]
    [SerializeField] public float minTimeBetween = 0.3f;
    [SerializeField] public float maxTimeBetween = 0.6f;

    [SerializeField] public float minTimeBetweenBound = 0.3f;
    [SerializeField] public float maxTimeBetweenBound = 0.6f;
    
    private float timeSinceLast; // Time since the last footstep sound
    private int StepCount = 0;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayMusic(music_Bg);

    }


    public void PlayMusic(AudioClip audioClip)
    {
        musicSource.clip = audioClip;
        musicSource.Play();

    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    public void PlayScaleUpSFX()
    {
        if (Time.time - timeSinceLast >= Random.Range(minTimeBetween, maxTimeBetween))
        {
            AudioClip sliceStepSound = scaleUp_sfx[Random.Range(0, scaleUp_sfx.Length)];
            sfxSource.PlayOneShot(sliceStepSound);
            timeSinceLast = Time.time;
        }
    }

     public void PlayBoundSFX()
    {
        if (Time.time - timeSinceLast >= Random.Range(minTimeBetweenBound, maxTimeBetweenBound))
        {
            AudioClip sliceStepSound = bound_sfx[Random.Range(0, bound_sfx.Length)];
            sfxSource.PlayOneShot(sliceStepSound);
            timeSinceLast = Time.time;
        }
    }
}
