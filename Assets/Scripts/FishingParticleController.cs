using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishingParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _splashParticleSystem;
    [SerializeField] private ParticleSystem _rippleParticleSystem;

    private void Start()
    {
        GameTimer.GetInstance().OnGlobalTimerEnded += OnGlobalTimerEnded;
        GameEndMenuUI.OnGameRestart += OnGameRestart;
        FoodSpawner.OnFoodBlockSpawned += OnFoodBlockSpawned;
    }

    private IEnumerator RestartRipple()
    {
        _rippleParticleSystem.Stop();
        yield return new WaitForSeconds(0.65f);
        _rippleParticleSystem.Play();
    }

    private void OnFoodBlockSpawned(object sender, EventArgs empty)
    {
        _splashParticleSystem.Play();
        StartCoroutine(RestartRipple());
    }

    private void OnGlobalTimerEnded(object sender, EventArgs empty)
    {
        _rippleParticleSystem.Stop();
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        _rippleParticleSystem.Play();
    }

    private void OnDestroy()
    {
        GameTimer.GetInstance().OnGlobalTimerEnded -= OnGlobalTimerEnded;
        FoodSpawner.OnFoodBlockSpawned -= OnFoodBlockSpawned;
    }
}
