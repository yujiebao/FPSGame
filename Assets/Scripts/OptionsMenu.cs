using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider volumeSlider;
    private void Start()
    {
        audioSource.volume = volumeSlider.value;
    }

    public void SetQuality(int quality)
    {  
        QualitySettings.SetQualityLevel(quality);
    }       

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
