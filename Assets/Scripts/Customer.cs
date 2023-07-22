using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Customer : MonoBehaviour
{
    public event EventHandler<int> OnCustomerGone;
    public event EventHandler<bool> OnCustomerMovementChanged;
    public static event EventHandler OnCustomerServed;
    public static event EventHandler OnCustomerSuccess;
    public static event EventHandler OnCustomerFail;

    [SerializeField] private TextMeshPro _foodNeededText;
    [SerializeField] private SpriteRenderer _speechBubble;
    [SerializeField] private Renderer _bodyRenderer;
    [SerializeField] private List<Material> _clothMaterials;

    private Transform _transform;
    private GoalTable _goalTable;
    private int _position;
    private bool _isMovingIn;
    private int _foodNeeded;

    public void Setup(int position, GoalTable goalTable, int foodNeeded)
    {
        _transform = transform;
        Material[] materials = _bodyRenderer.materials;
        materials[0] = _clothMaterials[UnityEngine.Random.Range(0, _clothMaterials.Count)];
        _bodyRenderer.materials = materials;
        _foodNeeded = foodNeeded;
        _foodNeededText.text = "";
        _speechBubble.gameObject.SetActive(false);
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
        _speechBubble.gameObject.SetActive(true);
        _goalTable.SetCanReceiveFood(true);
    }

    private IEnumerator MoveOut()
    {
        _goalTable.SetCanReceiveFood(false);
        _foodNeededText.text = "";
        _speechBubble.gameObject.SetActive(false);
        _transform.rotation = Quaternion.Euler(0f, 0f, 0f); //TODO change for lerp?
        float moveOutSpeed = 5f;
        OnCustomerMovementChanged?.Invoke(this, true);
        while (_transform.position.z < 16f)
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
            if (_foodNeeded == 0)
            {
                OnCustomerSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnCustomerFail?.Invoke(this, EventArgs.Empty);
            }
            StartCoroutine(MoveOut());
        }
        else
        {
            OnCustomerServed?.Invoke(this, EventArgs.Empty);
            _foodNeededText.text = _foodNeeded.ToString();
        }
    }
}
