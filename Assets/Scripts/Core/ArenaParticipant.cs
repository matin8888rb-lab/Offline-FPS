using UnityEngine;

namespace OfflineFPS.Core
{
    public class ArenaParticipant : MonoBehaviour
    {
        [SerializeField] private string participantId;
        [SerializeField] private TeamId team = TeamId.Blue;
        public string ParticipantId => string.IsNullOrWhiteSpace(participantId) ? gameObject.name : participantId;
        public TeamId Team => team;
        public void Configure(string id, TeamId assignedTeam) { participantId = id; team = assignedTeam; }
    }
}