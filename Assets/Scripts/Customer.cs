using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Customer : MonoBehaviour
{
    public event EventHandler<int> OnCustomerGone;
    public event EventHandler<bool> OnCustomerMovementChanged;
    public static event EventHandler<int> OnCustomerServed;

    [SerializeField] private TextMeshPro _foodNeededText;

    private Transform _transform;
    private GoalTable _goalTable;
    private int _position;
    private bool _isMovingIn;
    private int _foodNeeded;

    public void Setup(int position, GoalTable goalTable)
    {
        _transform = transform;
        _foodNeeded = UnityEngine.Random.Range(1, 6);
        _foodNeededText.text = "";
        _position = position;
        _goalTable = goalTable;
        _goalTable.OnFoodBlockReceived += OnFoodBlockReceived;
        StartCoroutine(MoveIn());
    }

    private IEnumerator MoveIn()
    {
        _isMovingIn = true;
        float moveInSpeed = -5f;
        OnCustomerMovementChanged?.Invoke(this, true);
        while (_isMovingIn)
        {
            _transform.position = new Vector3(
                _transform.position.x,
                _transform.position.y,
                _transform.position.z + (moveInSpeed * Time.deltaTime)
            );
            yield return new WaitForFixedUpdate();
        }
        _foodNeededText.text = _foodNeeded.ToString();
        _goalTable.SetCanReceiveFood(true);
    }

    private IEnumerator MoveOut()
    {
        _goalTable.SetCanReceiveFood(false);
        _foodNeededText.text = "";
        _transform.rotation = Quaternion.Euler(0f, 0f, 0f); //TODO change for lerp?
        float moveOutSpeed = 5f;
        OnCustomerMovementChanged?.Invoke(this, true);
        while (_transform.position.z < 15f)
        {
            _transform.position = new Vector3(
                _transform.position.x,
                _transform.position.y,
                _transform.position.z + (moveOutSpeed * Time.deltaTime)
            );
            yield return new WaitForFixedUpdate();
        }
        OnCustomerGone?.Invoke(this, _position);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _isMovingIn = false;
        OnCustomerMovementChanged?.Invoke(this, false);
    }

    private void OnDestroy()
    {
        _goalTable.OnFoodBlockReceived -= OnFoodBlockReceived;
    }

    private void OnFoodBlockReceived(object sender, int foodAmount)
    {
        _foodNeeded -= foodAmount;
        if (_foodNeeded <= 0)
        {
            int score = (_foodNeeded == 0) ? 3 : _foodNeeded;
            OnCustomerServed?.Invoke(this, score);
            StartCoroutine(MoveOut());
        }
        else
        {
            _foodNeededText.text = _foodNeeded.ToString();
        }
    }
}
