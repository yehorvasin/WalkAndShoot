using System;
using Source.Game.Input;
using UnityEngine;

namespace Source.Game.Player
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private GameObject _explosion;

        private PlayerMover _mover;
        private PlayerShooter _shooter;
        private Health.Health _health;

        public event Action PlayerDied;
        
        private void Start()
        {
            _mover = GetComponent<PlayerMover>();
            _shooter = GetComponent<PlayerShooter>();
            _health = GetComponent<Health.Health>();

            _inputController.MoveInputPerformed += OnMoveInputPerformed;
            _inputController.ShootInputPerformed += OnShootInputPerformed;
            
            _health.Died += OnPlayerDied;
        }

        private void OnMoveInputPerformed(Vector2 inputVector)
        {
            if (_health.IsDead)
            {
                _mover.Move(Vector3.zero);
                return;
            }
            
            var moveVector = new Vector3(inputVector.x, 0, inputVector.y);
            _mover.Move(moveVector);
        }
        
        private void OnShootInputPerformed(Vector2 inputVector)
        {
            if (_health.IsDead)
            {
                _shooter.Shoot(Vector3.zero);
                return;
            }

            inputVector.Normalize();
            var lookVector = new Vector3(inputVector.x, 0, inputVector.y);
            _shooter.Shoot(lookVector);
        }
        
        private void OnPlayerDied()
        {
            _explosion.SetActive(true);
            
            PlayerDied?.Invoke();
        }

        public void Reset()
        {
            _health.Refresh();
            _explosion.SetActive(false);
        }
    }
}
