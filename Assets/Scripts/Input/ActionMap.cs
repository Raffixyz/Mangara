
using UnityEngine;

namespace Input
{
    public abstract class ActionMap : IState
    {
        protected InputActions InputActions;
        public abstract bool HasPollable { get; }
        public ActionMap(InputActions action)
        {
            InputActions = action;
        }
        public abstract void OnEnter();

        public abstract void OnExit();

        public virtual void OnUpdate()
        {
        }
    }

    public class UIActionMap : ActionMap
    {
        private InputButton _inventory;
        public override bool HasPollable => false;
        public InputButton Inventory => _inventory;
        public UIActionMap(InputActions action) : base(action)
        {
            _inventory = new InputButton(action.UI.Inventory);
        }
        public override void OnEnter()
        {
            InputActions.UI.Enable();
        }

        public override void OnExit()
        {
            InputActions.UI.Disable();
        }
        public override void OnUpdate()
        {
        }
    }

    public class PlayerActionMap : ActionMap
    {
        private InputValue<Vector2> _movement;
        private InputButton _interact;
        private InputButton _inventory;
        public InputValue<Vector2> Movement => _movement;
        public InputButton Interact => _interact;
        public InputButton Inventory => _inventory;
        public override bool HasPollable => true;

        public PlayerActionMap(InputActions action) : base(action)
        {
            _movement = new InputValue<Vector2>(action.Player.Move);
            _interact = new InputButton(action.Player.Interact);
            _inventory = new InputButton(action.Player.Inventory);
        }


        public override void OnEnter()
        {
            InputActions.Player.Enable();
        }

        public override void OnExit()
        {
            InputActions.Player.Disable();
        }
        public override void OnUpdate()
        {
            _movement.ForcePoll();
        }
    }
    
}