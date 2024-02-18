using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(Instance);
    }

    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip backroundClip;

    private void Start()
    {
        musicAudioSource.volume = SaveAndLoad.GetMusicValue();
        sfxAudioSource.volume = SaveAndLoad.GetSFXValue();
    }

    public void PlayBackroundMusic()
    {
        musicAudioSource.clip = backroundClip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlayCollectCoinEffect()
    {
        sfxAudioSource.PlayOneShot(coinClip);
    }

    public void SetMusicVolume(float volume)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = volume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = volume;
        }
    }
}