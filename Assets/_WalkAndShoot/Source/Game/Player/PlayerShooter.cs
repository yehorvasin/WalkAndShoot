using Source.Game.Enemies;
using Source.Game.Utils;
using UnityEngine;

namespace Source.Game.Player
{
    public sealed class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Transform _gun;
        [SerializeField] private Transform _bulletsStartPoint;
        [SerializeField] private float _fireRate;
        
        [SerializeField] private PrefabFactory _bulletsFactory;

        private Vector3 _lookVector;
        private float _timeFromLastFire = Mathf.Infinity;

        private void Update()
        {
            if (_lookVector.Equals(Vector3.zero))
                return;
            
            if (_timeFromLastFire >= _fireRate)
            {
                var bullet = _bulletsFactory.GetNewObject(_bulletsStartPoint.position, _bulletsStartPoint.rotation);
                bullet.GetComponent<Bullet>().StartBehaviour();
                _timeFromLastFire = 0;
            }

            _timeFromLastFire += Time.deltaTime;
        }

        public void Shoot(Vector3 lookVector)
        {
            _lookVector = lookVector;
            
            if (_lookVector.Equals(Vector3.zero))
                return;
            
            _gun.rotation = Quaternion.LookRotation(_lookVector);
        }
    }
}
