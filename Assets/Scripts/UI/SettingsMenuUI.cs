using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);
        _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);
    }

    public void OnShow()
    {
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);;
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);;
    }

    private void OnMusicVolumeSliderChanged(float value)
    {
        value = Mathf.Clamp(value, 0.001f, 1f);
        PlayerPrefs.SetFloat("MusicVolume", value);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    private void OnSFXVolumeSliderChanged(float value)
    {
        value = Mathf.Clamp(value, 0.001f, 1f);
        PlayerPrefs.SetFloat("SFXVolume", value);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }
}
