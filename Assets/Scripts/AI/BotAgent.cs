using UnityEngine;
using UnityEngine.AI;

namespace OfflineFPS.AI
{
    // Friendly arena rival: follows a waypoint circuit and competes for checkpoints.
    [RequireComponent(typeof(NavMeshAgent))]
    public class BotAgent : MonoBehaviour
    {
        [SerializeField] private Transform[] routePoints;
        [SerializeField, Min(0.1f)] private float arriveDistance = 0.8f;
        private NavMeshAgent agent;
        private int nextPoint;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = 3.2f;
            agent.angularSpeed = 360f;
            agent.acceleration = 12f;
        }

        private void Start()
        {
            nextPoint = 0;
            SetNextDestination();
        }

        private void Update()
        {
            if (routePoints == null || routePoints.Length == 0 || agent == null || !agent.isOnNavMesh)
                return;

            if (!agent.pathPending && agent.remainingDistance <= arriveDistance)
            {
                nextPoint = (nextPoint + 1) % routePoints.Length;
                SetNextDestination();
            }
        }

        private void SetNextDestination()
        {
            if (agent == null || !agent.isOnNavMesh || routePoints == null || routePoints.Length == 0)
                return;

            for (int i = 0; i < routePoints.Length; i++)
            {
                Transform point = routePoints[nextPoint];
                if (point != null)
                {
                    agent.SetDestination(point.position);
                    return;
                }
                nextPoint = (nextPoint + 1) % routePoints.Length;
            }
        }

        public void SetRoute(Transform[] points)
        {
            routePoints = points;
            nextPoint = 0;
            SetNextDestination();
        }
    }
}