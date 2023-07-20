using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishermanAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _fishermanAnimator;

    private void Start()
    {
        GameEndMenuUI.OnGameRestart += OnGameRestart;
        FoodSpawner.OnFoodBlockSpawned += OnFoodBlockSpawned;
    }

    private void OnFoodBlockSpawned(object sender, EventArgs empty)
    {
        _fishermanAnimator.SetTrigger("ReelFishrod");
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        _fishermanAnimator.SetTrigger("ReelFishrod");
    }

    private void OnDestroy()
    {
        GameEndMenuUI.OnGameRestart -= OnGameRestart;
    }
}
