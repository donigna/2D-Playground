using Kuwiku.Basic2D;
using UnityEngine;
using ActionButtonState = com.Kuwiku.Basic2D.IInputReader.ActionButtonState;

namespace com.Kuwiku.Basic2D
{
    /// <summary>
    ///  LocalInputReader is responsible for reading player input from the local device.
    /// It uses the GameInput system to capture movement and jump actions.
    ///  It implements the IInputReader interface to provide input data to the PlayerController.
    /// This script is designed to work with Unity's new Input System.
    /// </summary>
    public class LocalInputReader : MonoBehaviour, IInputReader
    {
        private GameInput _gameInput;

        #region Private Variables for Actions
        private Vector2 _move;
        private bool _jumpPressed;
        private bool _actionButtonPressed;
        #endregion

        private void Awake()
        {
            _gameInput = new GameInput();
            EnableInput();

            _gameInput.Player.Move.performed += ctx => _move = ctx.ReadValue<Vector2>();
            _gameInput.Player.Move.canceled += ctx => _move = Vector2.zero;

            _gameInput.Player.Jump.performed += ctx => _jumpPressed = true;
            _gameInput.Player.Jump.canceled += ctx => _jumpPressed = false;

            _gameInput.Player.Action.performed += ctx => _actionButtonPressed = true;
            _gameInput.Player.Action.canceled += ctx => _actionButtonPressed = false;
        }

        public void DisableInput()
        {
            _gameInput.Disable();
        }

        public void EnableInput()
        {
            _gameInput.Enable();
        }


        #region Action Callbacks
        public Vector2 ReadMovement() { return _move; }

        public bool IsJumpPerformed() { return _jumpPressed; }

        public ActionButtonState GetActionButtonState()
        {
            if (_actionButtonPressed)
            {
                _actionButtonPressed = false; // Reset after reading
                return ActionButtonState.Pressed;
            }
            else if (_gameInput.Player.Action.IsPressed())
            {
                return ActionButtonState.Held;
            }
            else
            {
                return ActionButtonState.Released;
            }
        }
        #endregion
    }
}
