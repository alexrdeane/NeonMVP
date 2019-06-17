using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to audioMixer
    public Dropdown resolutionDropdown; // Reference to Dropdown
    Resolution[] resolutions; // Reference of Resolutions

    void Start()
    {
        resolutions = Screen.resolutions; // List of resolutions
        resolutionDropdown.ClearOptions(); // Default Resolutions
        int currentResolutionIndex = 0; // Current Resolution

        List<string> options = new List<string>(); // Loop though for each resolution array
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            // Function of Current Resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Refresh and Show Current Resolution
        resolutionDropdown.AddOptions(options); // Add Default Resolutions
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Update Resolution when Changing
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume (float volume) // Reference to Volume
    {
        // If you want to see the debug
        //Debug.Log(volume); 
        audioMixer.SetFloat("Volume", volume); // Function of audioMixer
    }

    public void SetQuality (int qualityIndex) // Reference to Quality
    {
        QualitySettings.SetQualityLevel(qualityIndex); // Function of Set Quality
    }

    public void SetFullScreen (bool isFullscreen) // Reference to Fullscreen
    {
        Screen.fullScreen = isFullscreen; // Function of Fullscreen
    }
}