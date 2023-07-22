using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SFXAudioManager : Singleton<SFXAudioManager>
{
    [SerializeField] private AudioClip _fishSpawnSound;
    [SerializeField] private AudioClip _deliverSound;
    [SerializeField] private AudioClip _customerSuccessSound;
    [SerializeField] private AudioClip _customerFailSound;
    [SerializeField] private AudioClip _buttonSound;

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
}
