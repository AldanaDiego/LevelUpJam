using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private Transform _transform;
    private int _position;
    private bool _isMoving;
    private float _speed;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _transform.position = new Vector3(
                _transform.position.x,
                _transform.position.y,
                _transform.position.z + (_speed * Time.deltaTime)
            );
        }
    }

    public void Setup(int position)
    {
        _position = position;
        MoveIn();
    }

    private void MoveIn()
    {
        _isMoving = true;
        _speed = -5f;
    }

    private void MoveOut()
    {
        _isMoving = true;
        _speed = 5f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _isMoving = false;
    }
}
