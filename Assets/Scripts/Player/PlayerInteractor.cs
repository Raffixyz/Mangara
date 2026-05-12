using System;
using Input;
using UnityEngine;

namespace Player
{
    public class PlayerInteractor: MonoBehaviour
    {
        private IInteractable _currentInteractable;

        public event Action<IInteractable> OnInteractionFound,  OnInteractionLost;
        
        private void OnEnable()
        {
            InputManager.Instance.PlayerInput.Interact.OnDown += OnInteract;
            InputManager.Instance.UIInput.Submit.OnDown += OnInteract;
        }

        private void OnDisable()
        {
            InputManager.Instance.PlayerInput.Interact.OnDown -= OnInteract;
            InputManager.Instance.UIInput.Submit.OnDown -= OnInteract;
        }

        private void OnInteract()
        {
            _currentInteractable?.Interact();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                _currentInteractable = interactable;
                OnInteractionFound?.Invoke(interactable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                if (_currentInteractable == interactable)
                {
                    _currentInteractable = null;
                    OnInteractionLost?.Invoke(interactable);
                }
            }
        }
    }
}