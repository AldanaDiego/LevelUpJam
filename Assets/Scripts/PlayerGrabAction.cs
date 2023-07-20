using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerGrabAction : MonoBehaviour
{
    public event EventHandler OnBlockGrabbed;
    public event EventHandler OnBlockDelivered;

    private const float GRAB_REACH = 1f;
    [SerializeField] private Transform _grabPoint;
    private InputSystem _inputSystem;
    private FoodBlock _foodBlock;
    private Transform _transform;

    private void Start()
    {
        _inputSystem = InputSystem.GetInstance();
        _foodBlock = null;
        _transform = transform;
        GameEndMenuUI.OnGameRestart += OnGameRestart;
    }

    private void Update()
    {
        if (_inputSystem.IsGrabTriggered())
        {
            if (_foodBlock == null)
            {
                if (Physics.Raycast(_transform.position, _transform.forward, out RaycastHit hit, GRAB_REACH) &&
                    hit.collider.tag == "FoodBlock"
                )
                {
                    FoodBlock foodBlock = hit.collider.GetComponent<FoodBlock>();
                    foodBlock.OnGrabbed(_grabPoint);
                    _foodBlock = foodBlock;
                    OnBlockGrabbed?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                if (Physics.Raycast(_transform.position, _transform.forward, out RaycastHit hit, GRAB_REACH) &&
                    hit.collider.tag == "GoalTable"
                )
                {
                    GoalTable goal = hit.collider.GetComponent<GoalTable>();
                    if (goal.CanReceiveFood())
                    {
                        goal.Receive(_foodBlock.GetFoodAmount());
                        Destroy(_foodBlock.gameObject);
                        _foodBlock = null;
                        OnBlockDelivered?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }   
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        if (_foodBlock != null)
        {
            Destroy(_foodBlock.gameObject);
            _foodBlock = null;
        }

    }

    private void OnDestroy()
    {
        GameEndMenuUI.OnGameRestart -= OnGameRestart;    
    }
}
