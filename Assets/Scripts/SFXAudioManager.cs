using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SFXAudioManager : Singleton<SFXAudioManager>
{
    [SerializeField] private AudioClip _fishSpawnSound;
    [SerializeField] private AudioClip _deliverSound;
    [SerializeField] private AudioClip _customerSuccessSound;
    [SerializeField] private AudioClip _customerFailSound;
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private AudioClip _clockTickSound;

    private AudioSource _sfxAudioSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _sfxAudioSource = GetComponent<AudioSource>();
        Customer.OnCustomerServed += OnCustomerServed;
        Customer.OnCustomerSuccess += OnCustomerSuccess;
        Customer.OnCustomerFail += OnCustomerFail;
        FoodSpawner.OnFoodBlockSpawned += OnFoodBlockSpawned;
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    public void OnButtonClicked()
    {
        _sfxAudioSource.PlayOneShot(_buttonSound);
    }

    private void OnCustomerServed(object sender, EventArgs empty)
    {
        _sfxAudioSource.PlayOneShot(_deliverSound);
    }

    private void OnCustomerSuccess(object sender, EventArgs empty)
    {
        _sfxAudioSource.PlayOneShot(_customerSuccessSound);
    }

    private void OnCustomerFail(object sender, EventArgs empty)
    {
        _sfxAudioSource.PlayOneShot(_customerFailSound);
    }

    private void OnFoodBlockSpawned(object sender, EventArgs empty)
    {
        _sfxAudioSource.PlayOneShot(_fishSpawnSound);
    }

    private void OnTenSecondsLeft(object sender, EventArgs empty)
    {
        _sfxAudioSource.PlayOneShot(_clockTickSound);
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode sceneMode)
    {
        GameTimer timer = GameTimer.GetInstance();
        if (timer != null)
        {
            timer.OnTenSecondsLeft += OnTenSecondsLeft;
        }
    }
}
