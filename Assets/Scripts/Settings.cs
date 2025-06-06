using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class Settings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public AudioMixerGroup targetGroup;
    public SoundManager soundManager;
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundSlider;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        // Get all available resolutions and populate the dropdown
        //resolutions = Screen.resolutions;
        //resolutionDropdown.ClearOptions();
        //List<string> options = new List<string>();
        //int currentResolutionIndex = 0;
        //for (int i = 0; i < resolutions.Length; i++)
        //{
        //    string option = resolutions[i].width + " x " + resolutions[i].height + ", " + Math.Round(resolutions[i].refreshRateRatio.value, 1) + "Hz";
        //    options.Add(option);
        //
        //    if (resolutions[i].width == Screen.currentResolution.width &&
        //        resolutions[i].height == Screen.currentResolution.height)
        //    {
        //        currentResolutionIndex = i;
        //    }
        //}
        //resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();

        soundManager = SoundManager.instance;
        mixer.GetFloat("Master", out float masterVolume);
        mixer.GetFloat("Sound", out float soundVolume);
        mixer.GetFloat("Music", out float musicVolume);
        masterSlider.value = Mathf.Pow(10, (masterVolume / 20));
        soundSlider.value = Mathf.Pow(10, (soundVolume / 20));
        musicSlider.value = Mathf.Pow(10, (musicVolume / 20));  
    }

    private void Awake()
    {

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log(isFullscreen);
    }

    //public void SetResolution(int resolutionIndex)
    //{
    //    Debug.Log("New resolution: " + resolutions[resolutionIndex]);
    //    Resolution resolution = resolutions[resolutionIndex];
    //    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    //}

    public void SetLevel(float sliderValue)
    {
        if (soundManager != null)
        {
            soundManager.SetVolume(targetGroup, sliderValue);
        }
    }

    public void SetMasterLevel(float sliderValue)
    {
        if (soundManager != null)
        {
            soundManager.SetVolume(mixer.FindMatchingGroups("Master")[0], sliderValue);
        } 
        else
        {
            mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("Master", sliderValue);
        }
    }

    public void SetSoundLevel(float sliderValue)
    {
        if (soundManager != null)
        {
            soundManager.SetVolume(mixer.FindMatchingGroups("Sound")[0], sliderValue);
        }
        else
        {
            mixer.SetFloat("Sound", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("Sound", sliderValue);
        }
    }
    public void SetMusicLevel(float sliderValue)
    {
        if (soundManager != null)
        {
            soundManager.SetVolume(mixer.FindMatchingGroups("Music")[0], sliderValue);
        }
        else
        {
            mixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("Music", sliderValue);
        }
    }
    
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Master", masterSlider.value);
        PlayerPrefs.SetFloat("Sound", soundSlider.value);
        PlayerPrefs.SetFloat("Music", musicSlider.value);
        PlayerPrefs.Save();
    }
}
