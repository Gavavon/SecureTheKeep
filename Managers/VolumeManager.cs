using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer mixer;

    public void setLevelMaster(float sliderVal)
    {

        mixer.SetFloat("MusicVol", (Mathf.Log10(sliderVal) * 20));
    }
    public void setLevelMusic(float sliderVal) 
    {

        mixer.SetFloat("MusicVol", (Mathf.Log10(sliderVal)*20));
    }
    public void setLevelSFX(float sliderVal)
    {

        mixer.SetFloat("MusicVol", (Mathf.Log10(sliderVal) * 20));
    }
}
