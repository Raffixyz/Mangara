using System;
using Input;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _playerObj;
    [SerializeField] private Transform _orientation;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        var viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = viewDir.normalized;
        
        var movement = InputManager.Instance.PlayerInput.Movement.Get();
        var horizontalInput = movement.x;
        var verticalInput = movement.y;
        
        var inputDir = _orientation.forward * verticalInput + _orientation.right * horizontalInput;
        
        
        if (inputDir != Vector3.zero)
        {
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir.normalized, Time.deltaTime * _rotationSpeed);
        }
            
    }
}
