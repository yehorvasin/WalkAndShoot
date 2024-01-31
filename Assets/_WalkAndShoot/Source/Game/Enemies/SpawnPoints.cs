using UnityEngine;
using Random = System.Random;

namespace Source.Game.Enemies
{
    public sealed class SpawnPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;

        public Transform GetRandomSpawnPoint()
        {
            Random random = new Random();
            int index = random.Next(0, _spawnPoints.Length);
            return _spawnPoints[index];
        }
    }
}
