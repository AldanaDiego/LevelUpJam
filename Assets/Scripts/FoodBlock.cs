using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodBlock : MonoBehaviour
{
    private const float START_ACCELERATION = -75f;

    public static event EventHandler OnFoodBlockGrabbed;

    [SerializeField] private int _foodAmount;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Collider _collider;

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
        if (other.tag == "FoodBlock")
        {
            StopMovement();
        }
    }

    private void StopMovement()
    {
        _canMove = false;
        _rigidBody.velocity = Vector3.zero;
        _transform.position = new Vector3(
            ((float) Math.Round((double) _transform.position.x, 1)),
            _transform.position.y,
            _transform.position.z
        );
        
    }

    public int GetFoodAmount()
    {
        return _foodAmount;
    }

    public void OnGrabbed(Transform playerGrab)
    {
        _canMove = true;
        _collider.enabled = false;
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
