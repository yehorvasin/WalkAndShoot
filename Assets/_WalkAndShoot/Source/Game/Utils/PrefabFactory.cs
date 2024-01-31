using System.Collections.Generic;
using UnityEngine;

namespace Source.Game.Utils
{
    public sealed class PrefabFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        private readonly List<GameObject> _allGameObjects = new();

        public GameObject GetNewObject(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            foreach (var obj in _allGameObjects)
            {
                if (obj.activeSelf)
                    continue;

                obj.transform.position = position;
                obj.transform.rotation = rotation;
                return obj;
            }

            var newObj = Instantiate(_prefab, position, rotation, parent);
            _allGameObjects.Add(newObj);
            return newObj;
        }
    }
}
