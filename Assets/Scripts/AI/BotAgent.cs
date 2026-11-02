using UnityEngine;

namespace OfflineFPS.AI
{
    // Lightweight waypoint rival; no runtime navigation package or network required.
    public class BotAgent : MonoBehaviour
    {
        [SerializeField] private Transform[] routePoints;
        [SerializeField, Min(0.2f)] private float moveSpeed = 2.8f;
        [SerializeField, Min(0.1f)] private float arriveDistance = 0.45f;
        private int nextPoint;

        private void Update()
        {
            if (routePoints == null || routePoints.Length == 0) return;
            Transform target = routePoints[nextPoint];
            if (target == null) { Advance(); return; }

            Vector3 direction = target.position - transform.position;
            direction.y = 0f;
            if (direction.sqrMagnitude <= arriveDistance * arriveDistance) { Advance(); return; }

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion desired = Quaternion.LookRotation(direction.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, desired, 7f * Time.deltaTime);
                transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            }
        }

        private void Advance()
        {
            if (routePoints == null || routePoints.Length == 0) return;
            nextPoint = (nextPoint + 1) % routePoints.Length;
        }

        public void SetRoute(Transform[] points) { routePoints = points; nextPoint = 0; }
    }
}