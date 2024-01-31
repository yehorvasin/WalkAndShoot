using System;
using System.Collections;
using Source.Game.Enemies;
using Source.Game.Player;
using Source.Game.Utils;
using Source.Game.Views;
using UnityEngine;

namespace Source.Game
{
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform _playerStartPosition;
        [SerializeField] private Timer _gameTime;

        [SerializeField] private EnemySpawner _enemySpawner;

        [Header("UI Elements")]
        [SerializeField] private GameObject InputUi;
        [SerializeField] private GameOverView GameOverUi;
        [SerializeField] private GameObject PlayerHealthBar;
        
        private void Start()
        {
            _gameTime.StartTimer();
            _playerController.PlayerDied += OnPlayerDied;
            GameOverUi.OnRestartRequested += OnRestartRequested;
            
            StartGame();
        }

        private void OnPlayerDied()
        {
            GameOver();
        }

        private void GameOver()
        {
            _gameTime.StopTimer();

            InputUi.SetActive(false);
            
            _enemySpawner.StopEnemies();

            StartCoroutine(WaitForExplosion());
        }

        private IEnumerator WaitForExplosion()
        {
            yield return new WaitForSeconds(1f);
            
            var timeSpan = TimeSpan.FromSeconds(_gameTime.GetTimeValue);
            GameOverUi.SetText($"Game Over! Time played: {timeSpan:mm\\:ss}");
            GameOverUi.gameObject.SetActive(true);
            PlayerHealthBar.SetActive(false);
        }

        private void OnRestartRequested()
        {
            StartGame();
        }

        private void StartGame()
        {
            InputUi.SetActive(true);
            PlayerHealthBar.SetActive(true);
            GameOverUi.gameObject.SetActive(false);

            _playerController.transform.position = _playerStartPosition.position;
            _playerController.Reset();
            
            _gameTime.ResetTimer();
            _gameTime.StartTimer();
            
            _enemySpawner.ResetAllEnemies();
        }
    }
}
