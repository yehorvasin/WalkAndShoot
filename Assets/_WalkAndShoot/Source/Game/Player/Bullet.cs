using UnityEngine;

namespace Source.Game.Player
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private float _maxTravelTime = 2f;
        [SerializeField] private float _travelSpeed = 12;
        [SerializeField] private float _damage = 50;

        private float _travelTime;
        
        public void StartBehaviour()
        {
            _travelTime = 0;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_travelTime >= _maxTravelTime)
                gameObject.SetActive(false);

            _travelTime += Time.deltaTime;
            
            transform.Translate(_travelSpeed * transform.forward * Time.deltaTime, Space.World);
        }
        
        private void OnTriggerEnter(Collider enteredCollider)
        {
            if (enteredCollider.tag.Equals("Player"))
                return;

            var health = enteredCollider.GetComponent<Health.Health>();
            if (health != null)
                health.TakeDamage(_damage);
            
            gameObject.SetActive(false);
        }
    }
}
