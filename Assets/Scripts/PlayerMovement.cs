using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float MOVEMENT_SPEED = 6.5f;
    private const float MOVEMENT_BOUND_X = 7f;
    private const float MOVEMENT_BOUND_Z_UP = 5f;
    private const float MOVEMENT_BOUND_Z_DOWN = -2f;

    private InputSystem _inputSystem;
    private Transform _transform;

    private void Start()
    {
        _inputSystem = InputSystem.GetInstance();
        _transform = transform;
    }

    private void Update()
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
        }    
    }
}
