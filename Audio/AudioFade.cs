using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioFade
{
    public static float standardVol;
    public static void setStandardVolume(AudioSource audioSource) 
    {
        standardVol = audioSource.volume;
    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = startVolume;
    }
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        while (audioSource.volume <= 0)
        {
            audioSource.volume += standardVol * Time.deltaTime / FadeTime;

            yield return null;
        }
        if (!audioSource.isPlaying) 
        {
            audioSource.Play();
        }
        audioSource.volume = standardVol;
    }
    
}

