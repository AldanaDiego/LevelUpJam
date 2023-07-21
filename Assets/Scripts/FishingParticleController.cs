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
        GameEndMenuUI.OnGameRestart += OnGameRestart;
        FoodSpawner.OnFoodBlockSpawned += OnFoodBlockSpawned;
    }

    private IEnumerator RestartRipple()
    {
        _rippleParticleSystem.Stop();
        yield return new WaitForSeconds(FishermanAnimationController.REEL_ROOD_TIME);
        _rippleParticleSystem.Play();
    }

    private void OnFoodBlockSpawned(object sender, EventArgs empty)
    {
        _splashParticleSystem.Play();
        StartCoroutine(RestartRipple());
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        StartCoroutine(RestartRipple());
    }

    private void OnDestroy()
    {
        FoodSpawner.OnFoodBlockSpawned -= OnFoodBlockSpawned;
    }
}
