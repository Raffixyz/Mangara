using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Input
{
    public class InputManager : PersistentSingleton<InputManager>
    {
        private InputActions _inputActions;
        private FiniteStateMachine<ActionMap> _actionMapStates;
        private PlayerActionMap _player;
        private UIActionMap _ui;
        public PlayerActionMap PlayerInput => _player;
        public UIActionMap UIInput => _ui;

        private SchemeType _currentControlScheme;
        public SchemeType CurrentControlScheme => _currentControlScheme;

        protected override void Awake()
        {
            base.Awake();
            InitializedManager();
            // InitializePlayerInput();
        }

        private void InitializedManager()
        {
            _inputActions = new InputActions();
            _player = new PlayerActionMap(_inputActions);
            _ui = new UIActionMap(_inputActions);
            _actionMapStates = new FiniteStateMachine<ActionMap>(_player);
        }

        public void PlayerMode()
        {
            _actionMapStates.ChangeState(_player);
            Debug.Log("State change to playermode");
        }
        public void UIMode()
        {
            _actionMapStates.ChangeState(_ui);
            Debug.Log("State change to uiMode");
        }
        
        // private void InitializePlayerInput()
        // {
        //     PlayerInput playerInput = gameObject.AddComponent<PlayerInput>();
        //     playerInput.actions = _inputActions.asset;
        //     playerInput.defaultControlScheme = "Keyboard&Mouse";
        //     playerInput.onControlsChanged += OnControlsChanged;
        // }

        // private void OnControlsChanged(PlayerInput input)
        // {
        //     Debug.Log("Control scheme changed to: " + input.currentControlScheme);
        //     var scheme = input.currentControlScheme;
        //     _currentControlScheme = scheme == "Gamepad"? SchemeType.Gamepad : SchemeType.Keyboard;
        // }

        public enum SchemeType
        {
            Keyboard,
            Gamepad,
            TouchScreen
        }
    }
}