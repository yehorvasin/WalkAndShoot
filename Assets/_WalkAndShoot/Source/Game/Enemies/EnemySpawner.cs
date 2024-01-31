using System.Collections.Generic;
using Source.Game.Utils;
using UnityEngine;

namespace Source.Game.Enemies
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnPoints _spawnPoints;
        [SerializeField] private PrefabFactory _enemyFactory;

        [SerializeField] private float _timeToSpawn = 2f;
        
        private float _timeFromLastSpawn = Mathf.Infinity;

        private readonly List<EnemyController> _enemies = new();

        private bool _isStopped;
        
        private void Update()
        {
            if (_isStopped)
                return;
            
            if (_timeFromLastSpawn >= _timeToSpawn)
            {
                var whereTo = _spawnPoints.GetRandomSpawnPoint();
                var newEnemy = _enemyFactory.GetNewObject(whereTo.position, Quaternion.identity, transform);
                
                var enemyController = newEnemy.GetComponent<EnemyController>();
                if (!_enemies.Contains(enemyController))
                    _enemies.Add(enemyController);
                
                enemyController.StartBehaviour();
                
                _timeFromLastSpawn = 0;
            }

            _timeFromLastSpawn += Time.deltaTime;
        }

        public void StopEnemies()
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.gameObject.activeSelf)
                    enemy.StopBehaviour();
            }

            _isStopped = true;
        }

        public void ResetAllEnemies()
        {
            foreach (var enemy in _enemies)
                enemy.gameObject.SetActive(false);
            
            _isStopped = false;
        }
    }
}
