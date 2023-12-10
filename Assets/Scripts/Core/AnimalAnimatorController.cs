using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class AnimalAnimatorController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            var speed = _agent.velocity.magnitude;
            _animator.SetFloat(Speed, speed);
        }
    }
}