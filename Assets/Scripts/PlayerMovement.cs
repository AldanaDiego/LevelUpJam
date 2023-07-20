using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler<bool> OnMovementChanged;

    private const float MOVEMENT_SPEED = 6.5f;
    private const float MOVEMENT_BOUND_X = 7f;
    private const float MOVEMENT_BOUND_Z_UP = 4f;
    private const float MOVEMENT_BOUND_Z_DOWN = -3f;

    private InputSystem _inputSystem;
    private Transform _transform;
    private GameTimer _timer;
    private bool _isActive;
    private bool _isMoving;

    private void Start()
    {
        _inputSystem = InputSystem.GetInstance();
        _transform = transform;
        _isActive = false;
        _isMoving = false;
        _timer = GameTimer.GetInstance();
        _timer.OnGlobalTimerStarted += OnGlobalTimerStarted;
        _timer.OnGlobalTimerEnded += OnGlobalTimerEnded;
        GameEndMenuUI.OnGameRestart += OnGameRestart;
        PlayerGrabAction grabAction = GetComponent<PlayerGrabAction>();
        grabAction.OnBlockGrabbed += OnBlockGrabbed;
        grabAction.OnBlockDelivered += OnBlockDelivered;
    }

    private void Update()
    {
        if (_isActive)
        {
            Vector2 movement = _inputSystem.GetMovement();
            if (movement != Vector2.zero)
            {
                if (!_isMoving)
                {
                    _isMoving = true;
                    OnMovementChanged?.Invoke(this, _isMoving);
                }
                movement = movement * (MOVEMENT_SPEED * Time.deltaTime);
                _transform.position = new Vector3(
                    Mathf.Clamp(_transform.position.x + movement.x, MOVEMENT_BOUND_X * -1, MOVEMENT_BOUND_X),
                    0f,
                    Mathf.Clamp(_transform.position.z + movement.y, MOVEMENT_BOUND_Z_DOWN, MOVEMENT_BOUND_Z_UP)
                );
                _transform.forward = new Vector3(movement.x, 0f, movement.y).normalized; //TODO change for Lerp?
            }
            else if (_isMoving)
            {
                _isMoving = false;
                OnMovementChanged?.Invoke(this, _isMoving);
            }
        }
    }

    public void EnableMovement()
    {
        if (_timer.IsGlobalTimeRunning())
        {
            _isActive = true;
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

    private void OnBlockGrabbed(object sender, EventArgs empty)
    {
        _isActive = false;
        _isMoving = false;
    }

    private void OnBlockDelivered(object sender, EventArgs empty)
    {
        _isActive = false;
        _isMoving = false;
    }

    private void OnDestroy()
    {
        GameEndMenuUI.OnGameRestart -= OnGameRestart;    
    }
}
