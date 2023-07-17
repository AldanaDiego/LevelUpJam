using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBlock : MonoBehaviour
{
    private const float START_ACCELERATION = -55f;

    [SerializeField] private int _width;
    [SerializeField] private Rigidbody _rigidBody;

    private Transform _transform;
    private bool _isGrabbed;
    private bool _canMove;

    private void Start()
    {
        _transform = transform;
        _isGrabbed = false;
        _canMove = true;
    }

    private void OnEnable()
    {
        _rigidBody.AddForce(new Vector3(START_ACCELERATION , 0f, 0f));
    }

    private void Update()
    {
        if (transform.position.x <= FoodSpawner.START_FOOD_BLOCK_POS)
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
        transform.position = new Vector3(
            Mathf.Round(transform.position.x),
            transform.position.y,
            transform.position.z
        );
    }

    public int GetWidth()
    {
        return _width;
    }
}
