using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    private Resolution[] resolutions;

    void Start()
    {
        // Get available resolutions and add them to the resolution dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = -1;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height} @{resolutions[i].refreshRate}Hz";
            options.Add(option);
            Debug.Log($"Resolution: {resolutions[i].width} x {resolutions[i].height} @{resolutions[i].refreshRate}Hz");
            Debug.Log($"Current Resolution: {Camera.main.pixelWidth} x {Camera.main.pixelHeight} @{Screen.currentResolution.refreshRate}Hz");
            if (resolutions[i].width == Camera.main.pixelWidth &&
                resolutions[i].height == Camera.main.pixelHeight &&
                resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Load fullscreen setting and set toggle value 
        fullscreenToggle.isOn = PlayerPrefs.GetInt("IsFullscreen", 1) == 1;

        // Load volume setting and set slider value
        volumeSlider.value = PlayerPrefs.GetInt("volume", 0) / 10000f;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);

        PlayerPrefs.SetInt("ScreenWidth", resolution.width);
        PlayerPrefs.SetInt("ScreenHeight", resolution.height);
        PlayerPrefs.SetInt("RefreshRate", resolution.refreshRate);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        // Turn volume into int for Windows registry
        int volumeInt = Mathf.RoundToInt(volume * 10000f);
        PlayerPrefs.SetInt("volume", volumeInt);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("IsFullscreen", isFullscreen ? 1 : 0);
    }
}
