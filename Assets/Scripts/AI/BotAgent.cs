using UnityEngine;
using UnityEngine.AI;

namespace OfflineFPS.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BotAgent : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float retargetSeconds = 2f;
        private NavMeshAgent agent;
        private int nextPoint;
        private float timer;

        private void Awake() => agent = GetComponent<NavMeshAgent>();

        private void Update()
        {
            if (patrolPoints == null || patrolPoints.Length == 0 || agent == null || !agent.isOnNavMesh)
                return;

            timer -= Time.deltaTime;
            if (timer > 0f && agent.pathPending) return;
            if (timer <= 0f || (!agent.pathPending && agent.remainingDistance <= 0.5f))
            {
                nextPoint = (nextPoint + 1) % patrolPoints.Length;
                if (patrolPoints[nextPoint] != null)
                    agent.SetDestination(patrolPoints[nextPoint].position);
                timer = retargetSeconds;
            }
        }
    }
}
