using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OnClickOptionsMenu : MonoBehaviour {
    public AudioMixer audioMixer;
   

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void GraphicsQuality(Slider slider)
    {
        QualitySettings.SetQualityLevel((int)slider.value);
    }
    public void Update()
    {
        Debug.Log(QualitySettings.GetQualityLevel());
    }
}
