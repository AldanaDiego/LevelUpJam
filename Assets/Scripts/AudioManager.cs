using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip _fishSpawnSound;
    [SerializeField] private AudioClip _deliverSound;
    [SerializeField] private AudioClip _customerSuccessSound;
    [SerializeField] private AudioClip _customerFailSound;
    [SerializeField] private AudioClip _buttonSound;

    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Customer.OnCustomerServed += OnCustomerServed;
        Customer.OnCustomerSuccess += OnCustomerSuccess;
        Customer.OnCustomerFail += OnCustomerFail;
        FoodSpawner.OnFoodBlockSpawned += OnFoodBlockSpawned;
    }

    public void OnButtonClicked()
    {
        _audioSource.PlayOneShot(_buttonSound);
    }

    private void OnCustomerServed(object sender, EventArgs empty)
    {
        _audioSource.PlayOneShot(_deliverSound);
    }

    private void OnCustomerSuccess(object sender, EventArgs empty)
    {
        _audioSource.PlayOneShot(_customerSuccessSound);
    }

    private void OnCustomerFail(object sender, EventArgs empty)
    {
        _audioSource.PlayOneShot(_customerFailSound);
    }

    private void OnFoodBlockSpawned(object sender, EventArgs empty)
    {
        _audioSource.PlayOneShot(_fishSpawnSound);
    }
}
