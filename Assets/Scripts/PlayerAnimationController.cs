using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.OnMovementChanged += OnMovementChanged;
        PlayerGrabAction grabAction = GetComponent<PlayerGrabAction>();
        grabAction.OnBlockGrabbed += OnBlockGrabbed;
        grabAction.OnBlockDelivered += OnBlockDelivered;
        GameEndMenuUI.OnGameRestart += OnGameRestart;
        GameTimer timer = GameTimer.GetInstance();
        timer.OnGlobalTimerEnded += OnGlobalTimerEnded;
    }

    public void OnPickupEnded()
    {
        _playerMovement.EnableMovement();
    }

    public void OnDeliverEnded()
    {
        _playerMovement.EnableMovement();
    }

    private void OnMovementChanged(object sender, bool isMoving)
    {
        _playerAnimator.SetBool("IsMoving", isMoving);
    }

    private void OnBlockGrabbed(object sender, EventArgs empty)
    {
        _playerAnimator.SetTrigger("Grabbed");
    }

    private void OnBlockDelivered(object sender, EventArgs empty)
    {
        _playerAnimator.SetTrigger("Delivered");
    }

    private void OnGameRestart(object sender, EventArgs empty)
    {
        _playerAnimator.SetBool("IsMoving", false);
        _playerAnimator.SetTrigger("Restart");
    }

    private void OnGlobalTimerEnded(object sender, EventArgs empty)
    {
        _playerAnimator.SetBool("IsMoving", false);
    }

    private void OnDestroy()
    {
        GameEndMenuUI.OnGameRestart -= OnGameRestart;    
    }
}
