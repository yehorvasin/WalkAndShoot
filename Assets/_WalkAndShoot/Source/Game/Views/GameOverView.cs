using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Views
{
    public sealed class GameOverView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _restartButton;

        public event Action OnRestartRequested;
        
        private void Start()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            OnRestartRequested?.Invoke();
        }

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}
