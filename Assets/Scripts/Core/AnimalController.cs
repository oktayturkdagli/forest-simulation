using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class AnimalController : MonoBehaviour
    {
        [field: SerializeField] public float WanderRadius { get; set; } = 10f;
        [field: SerializeField] public float WanderTimer { get; set; } = 5f;
        
        private Transform _target;
        private NavMeshAgent _agent;
        private float _timer;
        
        private void OnEnable()
        {
            _agent = GetComponent<NavMeshAgent>();
            _timer = WanderTimer;
        }
        
        private void Update()
        {
            _timer += Time.deltaTime;
            
            if (_timer >= WanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, WanderRadius, -1);
                _agent.SetDestination(newPos);
                _timer = 0;
            }
        }

        private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;
            randDirection += origin;
            NavMesh.SamplePosition(randDirection, out var navHit, dist, layerMask);
            return navHit.position;
        }
    }
}