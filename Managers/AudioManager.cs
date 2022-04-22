using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public GameManagement gameManagement;

    [Header("Music")]
    public AudioClip[] menuMusic;
    public AudioClip[] normalMusic;
    public AudioClip[] wave10Music;
    public AudioClip deathMusic;

    private AudioClip currentMusic;

    [Header("SFX")]
    public AudioClip fireBallFX;
    public AudioClip blizzardFX;
    public AudioClip earthFX;
    public AudioClip VineFX;

    [Header("Universal")]
    public float audioFadeTime = 1f;

    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        setMusic();
        AudioFade.setStandardVolume(audioSource);
    }
    public void setMusic()
    {
        /*
        if (audioSource.isPlaying)
        {
            StartCoroutine(AudioFade.FadeOut(audioSource, audioFadeTime));
        }
        */
        audioSource.Stop();
        switch (gameManagement.getGameState()) 
        {
            case GameManagement.gameplayState.UIMenu:
                //StartCoroutine(AudioFade.FadeIn(audioSource, audioFadeTime));
                audioSource.clip = menuMusic[Random.Range(0, menuMusic.Length)];
                break;
            case GameManagement.gameplayState.Gameplay:
                //StartCoroutine(AudioFade.FadeIn(audioSource, audioFadeTime));
                audioSource.clip = normalMusic[Random.Range(0, normalMusic.Length)];
                break;
        }
        audioSource.Play();
    }
    public void waveMusicSet() 
    {
        setCurrentMusic();
        audioSource.clip = wave10Music[Random.Range(0, wave10Music.Length)];
    }
    public void deathMusicSet()
    {
        setCurrentMusic();
        audioSource.clip = deathMusic;
    }
    public void resetMusic() 
    {
        audioSource.clip = currentMusic;
    }
    private void setCurrentMusic() 
    {
        currentMusic = audioSource.clip;
    }
}
