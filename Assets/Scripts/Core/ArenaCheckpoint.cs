using System.Collections.Generic;
using UnityEngine;

namespace OfflineFPS.Core
{
    [RequireComponent(typeof(Collider))]
    public class ArenaCheckpoint : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float repeatDelay = 2f;
        private readonly Dictionary<string, float> nextAllowedTime = new Dictionary<string, float>();
        private MatchManager match;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
            match = FindObjectOfType<MatchManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            ArenaParticipant participant = other.GetComponentInParent<ArenaParticipant>();
            if (participant == null || match == null || match.IsEnded) return;
            string id = participant.ParticipantId;
            if (nextAllowedTime.TryGetValue(id, out float allowedAt) && Time.time < allowedAt) return;
            nextAllowedTime[id] = Time.time + repeatDelay;
            match.RegisterCheckpoint(id, participant.Team);
        }
    }
}