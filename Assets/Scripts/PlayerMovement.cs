using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private const float MOVEMENT_SPEED = 6.5f;
    private const float MOVEMENT_BOUND_X = 7f;
    private const float MOVEMENT_BOUND_Z_UP = 5f;
    private const float MOVEMENT_BOUND_Z_DOWN = -2f;

    private InputSystem _inputSystem;
    private Transform _transform;
    private bool _isActive;

    private void Start()
    {
        _inputSystem = InputSystem.GetInstance();
        _transform = transform;
        _isActive = false;
        GameTimer timer = GameTimer.GetInstance();
        timer.OnGlobalTimerStarted += OnGlobalTimerStarted;
        timer.OnGlobalTimerEnded += OnGlobalTimerEnded;
        GameEndMenuUI.OnGameRestart += OnGameRestart;
    }

    private void Update()
    {
        if (_isActive)
        {
            Vector2 movement = _inputSystem.GetMovement();
            if (movement != Vector2.zero)
            {
                movement = movement * (MOVEMENT_SPEED * Time.deltaTime);
                _transform.position = new Vector3(
                    Mathf.Clamp(_transform.position.x + movement.x, MOVEMENT_BOUND_X * -1, MOVEMENT_BOUND_X),
                    0f,
                    Mathf.Clamp(_transform.position.z + movement.y, MOVEMENT_BOUND_Z_DOWN, MOVEMENT_BOUND_Z_UP)
                );
                _transform.forward = new Vector3(movement.x, 0f, movement.y).normalized;
            }    
        }
    }

    private void OnGlobalTimerStarted(object sender, EventArgs empty)
    {
        _isActive = true;
    }

    private void OnGlobalTimerEnded(object sender, EventArgs empty)
    {
        _isActive = false;
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        _transform.position = Vector3.zero;
        _transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
}
