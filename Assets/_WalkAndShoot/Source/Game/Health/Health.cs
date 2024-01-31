using System;
using UnityEngine;

namespace Source.Game.Health
{
    public sealed class Health : MonoBehaviour
    {
        [SerializeField] private float _startingHealth = 100f;

        public bool IsDead { get; private set; }

        private float _currentHealth;
        public float CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value;
                Changed?.Invoke();
            }
        }

        public float StartingHealth => _startingHealth;
        
        public event Action Changed;
        public event Action Died;

        private void Start()
        {
            Refresh();
        }

        public void TakeDamage(float damageAmount)
        {
            if (IsDead)
                return;
            
            CurrentHealth = Mathf.Max(0, CurrentHealth - damageAmount);

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                Died?.Invoke();
            }
            
            Changed?.Invoke();
        }

        public void Refresh()
        {
            CurrentHealth = StartingHealth;
            IsDead = false;
            
            Changed?.Invoke();
        }
    }
}
