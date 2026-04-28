using System;
using Input;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _orientation;
    [SerializeField] private float _moveSpeed;
    
    private Rigidbody _rigidbody;
    private Vector2 _movement;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _movement = InputManager.Instance.PlayerInput.Movement.Get();
    }

    private void FixedUpdate()
    {
        Vector3 forward = _orientation.forward;
        Vector3 right = _orientation.right;
        
        Vector3 moveDirection = (forward * _movement.y + right * _movement.x).normalized;

        _rigidbody.MovePosition(_rigidbody.position + moveDirection * (_moveSpeed * Time.fixedDeltaTime));
    }
}
