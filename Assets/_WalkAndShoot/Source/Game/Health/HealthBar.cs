using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Health
{
    public sealed class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Image _healthBar;

        private void Start()
        {
            _health.Changed += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            _healthBar.fillAmount = _health.CurrentHealth / _health.StartingHealth;
        }
    }
}
