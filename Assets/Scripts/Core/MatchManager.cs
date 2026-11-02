using System;
using System.Collections.Generic;
using UnityEngine;

namespace OfflineFPS.Core
{
    public enum MatchMode { FreeForAll, TeamChallenge }
    public enum TeamId { Neutral, Blue, Orange }

    // Checkpoint-racing score manager for offline arena matches.
    public class MatchManager : MonoBehaviour
    {
        [Min(1)] public int scoreLimit = 8;
        public MatchMode mode = MatchMode.FreeForAll;
        public event Action<TeamId, int> TeamScoreChanged;
        public event Action<string, int> PlayerScoreChanged;
        public event Action<string> MatchEnded;

        private readonly Dictionary<string, int> playerScores = new Dictionary<string, int>();
        private int blueScore;
        private int orangeScore;
        private bool ended;
        public int BlueScore => blueScore;
        public int OrangeScore => orangeScore;
        public bool IsEnded => ended;

        public void StartMatch(MatchMode selectedMode, int limit = 8)
        {
            mode = selectedMode;
            scoreLimit = Mathf.Max(1, limit);
            blueScore = orangeScore = 0;
            playerScores.Clear();
            ended = false;
            TeamScoreChanged?.Invoke(TeamId.Blue, 0);
            TeamScoreChanged?.Invoke(TeamId.Orange, 0);
        }

        public void RegisterCheckpoint(string participantId, TeamId team)
        {
            if (ended || string.IsNullOrWhiteSpace(participantId)) return;
            if (mode == MatchMode.FreeForAll)
            {
                int score = playerScores.TryGetValue(participantId, out int previous) ? previous + 1 : 1;
                playerScores[participantId] = score;
                PlayerScoreChanged?.Invoke(participantId, score);
                if (score >= scoreLimit) End(participantId);
                return;
            }

            if (team == TeamId.Blue) blueScore++;
            else if (team == TeamId.Orange) orangeScore++;
            else return;

            TeamScoreChanged?.Invoke(TeamId.Blue, blueScore);
            TeamScoreChanged?.Invoke(TeamId.Orange, orangeScore);
            if (blueScore >= scoreLimit) End("Blue Team");
            else if (orangeScore >= scoreLimit) End("Orange Team");
        }

        private void End(string winner)
        {
            ended = true;
            MatchEnded?.Invoke(winner);
        }
    }
}