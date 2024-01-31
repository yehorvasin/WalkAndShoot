using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Game.Input
{
    public sealed class InputController : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;

        public event Action<Vector2> MoveInputPerformed;
        public event Action<Vector2> ShootInputPerformed;

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();

            _playerInputActions.Player.Move.performed += OnMoveInputPerformed;
            _playerInputActions.Player.Move.canceled += OnMoveInputPerformed;

            _playerInputActions.Player.Shoot.performed += OnShootInputPerformed;
            _playerInputActions.Player.Shoot.canceled += OnShootInputPerformed;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Move.performed -= OnMoveInputPerformed;
            _playerInputActions.Player.Move.canceled -= OnMoveInputPerformed;

            _playerInputActions.Player.Shoot.performed -= OnShootInputPerformed;
            _playerInputActions.Player.Shoot.canceled -= OnShootInputPerformed;
        }

        private void OnMoveInputPerformed(InputAction.CallbackContext callbackContext)
        {
            MoveInputPerformed?.Invoke(callbackContext.ReadValue<Vector2>());
        }

        private void OnShootInputPerformed(InputAction.CallbackContext callbackContext)
        {
            ShootInputPerformed?.Invoke(callbackContext.ReadValue<Vector2>());
        }
    }
}
