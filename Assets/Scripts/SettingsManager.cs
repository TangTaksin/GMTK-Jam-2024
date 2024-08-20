using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("===================Settings=================")]
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject settingPanal;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
    }

    private void Update()
    {
        // Restart level when 'R' is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        // Toggle settings panel when 'P' is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleSettingsPanel();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void LoadVolume()
    {
        Debug.Log("Loading volume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ToggleSettingsPanel()
    {
        // Toggle the active state of the settings panel
        settingPanal.SetActive(!settingPanal.activeSelf);

        // If the settings panel is active, pause the game time; otherwise, resume the game time
        if (settingPanal.activeSelf)
        {
            Time.timeScale = 0f;  // Pause the game
        }
        else
        {
            Time.timeScale = 1f;  // Resume the game
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
