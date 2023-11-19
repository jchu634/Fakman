using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenuScript : MonoBehaviour
{
    
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropDown;
    public TextMeshProUGUI QualityLabel;
    public TextMeshProUGUI ResolutionLabel;
    private void Start()
    {
        //Debug.Log(Globals.currentResolution.width + " " + Globals.currentResolution.height);
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int n = 0; n < resolutions.Length; n++)
        {
            string option = resolutions[n].width + "x" + resolutions[n].height;
            options.Add(option);
            if (resolutions[n].width == Screen.currentResolution.width && resolutions[n].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = n;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
        //Screen.SetResolution(Globals.currentResolution.width, Globals.currentResolution.height, Screen.fullScreen);
    }
    public void setResolution(int resolutionIndex)
    {
        //AspectRatioFitter
        //Screen.SetResolution(Globals.currentResolution.width, Globals.currentResolution.height, Screen.fullScreen);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Globals.currentResolution = resolution;
        ResolutionLabel.text = (resolution.width + "," + resolution.height);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
    public void setQuality(int quality) {
        QualitySettings.SetQualityLevel(quality);
        switch(QualitySettings.GetQualityLevel())
        {
            case 0:
                QualityLabel.text = "Low";
                break;
            case 1:
                QualityLabel.text = "Medium";
                break;
            case 2:
                QualityLabel.text = "High";
                break;
        }
        
    }

    public void setFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
