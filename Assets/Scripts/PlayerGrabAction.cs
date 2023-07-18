using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabAction : MonoBehaviour
{
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
                }
            }
            else
            {
                if (Physics.Raycast(_transform.position, _transform.forward, out RaycastHit hit, GRAB_REACH) &&
                    hit.collider.tag == "GoalTable"
                )
                {
                    Destroy(_foodBlock.gameObject);
                    _foodBlock = null;
                    //TODO event
                }
            }
        }   
    }
}
