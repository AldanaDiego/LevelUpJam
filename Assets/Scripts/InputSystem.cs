using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : Singleton<InputSystem>
{
    private PlayerInputActions _playerInput;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _playerInput = new PlayerInputActions();
        _playerInput.Enable();
    }

    public Vector2 GetMovement()
    {
        return _playerInput.PlayerActionMap.Move.ReadValue<Vector2>();
    }

    public bool IsGrabTriggered()
    {
        return _playerInput.PlayerActionMap.Grab.IsPressed();
    }
}
