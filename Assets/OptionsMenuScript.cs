using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenuScript : MonoBehaviour
{
    
    public AudioMixer audioMixer;
    public TextMeshProUGUI QualityLabel;
    private void Start()
    {
        //Debug.Log(Globals.currentResolution.width + " " + Globals.currentResolution.height);
        //Screen.SetResolution(Globals.currentResolution.width, Globals.currentResolution.height, Screen.fullScreen);
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

}
