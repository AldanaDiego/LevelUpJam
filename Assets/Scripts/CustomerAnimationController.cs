using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _customerAnimator;

    private Customer _customer;

    private void Awake()
    {
        _customer = GetComponent<Customer>();
        _customer.OnCustomerMovementChanged += OnCustomerMovementChanged;
    }

    private void OnCustomerMovementChanged(object sender, bool isMoving)
    {
        _customerAnimator.SetBool("IsMoving", isMoving);
    }
}
