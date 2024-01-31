using UnityEngine;
using UnityEngine.AI;

namespace Source.Game.Enemies
{
    public sealed class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _damage = 25f;
        
        private NavMeshAgent _agent;
        private Transform _target;
        private Health.Health _health;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _speed;

            _health = GetComponent<Health.Health>();
            _health.Died += OnDied;
        }

        private void Update()
        {
            if (_agent.isStopped || _target == null)
                return;
            
            _agent.destination = _target.position;

            if (Vector3.Distance(transform.position, _target.position) < 1)
            {
                DoDamage();
                Die();
            }
        }

        public void StartBehaviour()
        {
            gameObject.SetActive(true);
            
            _health.Refresh();
            
            _target = GameObject.Find("Player").transform;
            
            _agent.isStopped = false;
            _agent.destination = _target.position;
        }

        public void StopBehaviour()
        {
            _agent.isStopped = true;
        }
        
        private void DoDamage()
        {
            _target.GetComponent<Health.Health>().TakeDamage(_damage);
            gameObject.SetActive(false);
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }

        private void OnDied()
        {
            Die();
        }
    }
}
