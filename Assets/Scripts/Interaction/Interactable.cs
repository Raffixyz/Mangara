using System;
using Input;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract; 
    
    private bool _insideInteractArea;

    private void OnEnable()
    {
        InputManager.Instance.PlayerInput.Interact.OnDown += OnInteract;
    }

    private void OnDisable()
    {
        InputManager.Instance.PlayerInput.Interact.OnDown -= OnInteract;
    }

    private void OnInteract()
    {
        if (_insideInteractArea)
        {
            Debug.Log($"interacted with {transform.name}");
            onInteract?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCondition(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckCondition(other);
    }

    private void CheckCondition(Collider  other)
    {
        if (other.CompareTag("Player"))
        {
            _insideInteractArea = !_insideInteractArea;
        }
    }
}