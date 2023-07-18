using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodBlock : MonoBehaviour
{
    private const float START_ACCELERATION = -55f;

    public static event EventHandler OnFoodBlockGrabbed;

    [SerializeField] private int _width;
    [SerializeField] private int _foodAmount;
    [SerializeField] private Rigidbody _rigidBody;

    private Transform _transform;
    private bool _canMove;

    private void Start()
    {
        _transform = transform;
        _canMove = true;
    }

    private void OnEnable()
    {
        _rigidBody.AddForce(new Vector3(START_ACCELERATION , 0f, 0f));
    }

    private void Update()
    {
        if (_canMove && _transform.position.x <= FoodSpawner.START_FOOD_BLOCK_POS)
        {
            StopMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StopMovement();
    }

    private void StopMovement()
    {
        _canMove = false;
        _rigidBody.velocity = Vector3.zero;
        _transform.position = new Vector3(
            Mathf.Round(_transform.position.x),
            _transform.position.y,
            _transform.position.z
        );
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetFoodAmount()
    {
        return _foodAmount;
    }

    public void OnGrabbed(Transform playerGrab)
    {
        _canMove = true;
        _rigidBody.velocity = Vector3.zero;
        _transform.SetParent(playerGrab);
        _transform.localPosition = Vector3.zero;
        OnFoodBlockGrabbed?.Invoke(this, EventArgs.Empty);
    }

    public void Move()
    {
        if (!_canMove)
        {
            _canMove = true;
            _rigidBody.AddForce(new Vector3(START_ACCELERATION , 0f, 0f));
        }
    }
}
