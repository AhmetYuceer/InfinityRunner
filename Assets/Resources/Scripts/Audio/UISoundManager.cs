using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundManager : MonoBehaviour
{
    public static UISoundManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = SaveAndLoad.GetMusicValue();
        sfxSlider.value = SaveAndLoad.GetSFXValue();

        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
    }

    private void OnMusicSliderValueChanged(float value)
    {
        if (SoundManager.Instance.musicAudioSource != null && musicSlider != null)
        {
            SoundManager.Instance.sfxAudioSource.Stop();
            SoundManager.Instance.PlayBackroundMusic();
            SoundManager.Instance.SetMusicVolume(musicSlider.value);
        }
    }  
    private void OnSFXSliderValueChanged(float value)
    {
        if (SoundManager.Instance.sfxAudioSource != null && sfxSlider != null)
        {
            SoundManager.Instance.musicAudioSource.Stop();
            SoundManager.Instance.PlayCollectCoinEffect();
            SoundManager.Instance.SetSFXVolume(sfxSlider.value);
        }
    }

    public void SaveMusicSliderValue()
    {
        SaveAndLoad.SetMusicValue(musicSlider.value);
        SaveAndLoad.SetSFXValue(sfxSlider.value);
    }
}