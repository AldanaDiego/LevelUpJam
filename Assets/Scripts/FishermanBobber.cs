using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishermanBobber : MonoBehaviour
{
    [SerializeField] private Transform _fishingRodTip;
    [SerializeField] private Transform _bobber;
    [SerializeField] private Transform _bobberTip;
    [SerializeField] private Transform _fishingSpot;
    [SerializeField] private LineRenderer _fishingLine;

    private const float BOBBER_BOUND_UP = 0.1f;
    private const float BOBBER_BOUND_DOWN = -0.05f;
    private float _bobberSpeed = 0.15f;

    private void Start()
    {
        _bobber.position = _fishingSpot.position;
        SetupFishingLine();
        HideBobber();
        GameEndMenuUI.OnGameRestart += OnGameRestart;
        FoodSpawner.OnFoodBlockSpawned += OnFoodBlockSpawned;
    }

    private void OnEnable()
    {
        StartCoroutine(RespawnBobber());
    }

    private void Update()
    {
        if (_bobber.gameObject.activeSelf)
        {
            _bobber.position = new Vector3(
                _bobber.position.x,
                _bobber.position.y + (_bobberSpeed * Time.deltaTime),
                _bobber.position.z
            );
            _fishingLine.SetPosition(0, _fishingRodTip.position);
            _fishingLine.SetPosition(1, _bobberTip.position);

            if ((_bobberSpeed > 0f && _bobber.position.y >= BOBBER_BOUND_UP) ||
                (_bobberSpeed < 0f && _bobber.position.y <= BOBBER_BOUND_DOWN))
            {
                _bobberSpeed *= -1f;
            }
        }    
    }

    private void SetupFishingLine()
    {
        _fishingLine.useWorldSpace = true;
        _fishingLine.startWidth = 0.01f;
        _fishingLine.endWidth = 0.01f;
        _fishingLine.SetPositions(new List<Vector3>() {
            _fishingRodTip.position,
            _bobberTip.position
         }.ToArray());
    }

    private void ShowBobber()
    {
        _bobber.gameObject.SetActive(true);
        _fishingLine.gameObject.SetActive(true);
    }

    private void HideBobber()
    {
        _bobber.gameObject.SetActive(false);
        _fishingLine.gameObject.SetActive(false);
    }

    private IEnumerator RespawnBobber()
    {
        HideBobber();
        yield return new WaitForSeconds(FishermanAnimationController.REEL_ROOD_TIME);
        ShowBobber();
    }

    private void OnFoodBlockSpawned(object sender, EventArgs empty)
    {
        StartCoroutine(RespawnBobber());
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        StartCoroutine(RespawnBobber());
    }

    private void OnDestroy()
    {
        GameEndMenuUI.OnGameRestart -= OnGameRestart;
        FoodSpawner.OnFoodBlockSpawned -= OnFoodBlockSpawned;
    }

}
