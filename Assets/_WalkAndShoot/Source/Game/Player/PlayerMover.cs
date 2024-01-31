using UnityEngine;

namespace Source.Game.Player
{
    public sealed class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10f;
        
        private Vector3 _moveVector;

        private void Update()
        {
            transform.Translate(_moveSpeed * _moveVector * Time.deltaTime);
        }

        public void Move(Vector3 moveVector)
        {
            _moveVector = moveVector;
        }
    }
}
