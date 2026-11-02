using System;
using UnityEngine;

namespace OfflineFPS.Core
{
    public enum MatchMode { FreeForAll, TeamDeathmatch }
    public enum TeamId { Neutral, Blue, Orange }

    public class MatchManager : MonoBehaviour
    {
        [Min(1)] public int scoreLimit = 15;
        public MatchMode mode = MatchMode.FreeForAll;
        public event Action<TeamId, int> ScoreChanged;
        public event Action<TeamId> MatchEnded;

        private int blueScore;
        private int orangeScore;
        private bool ended;

        public int BlueScore => blueScore;
        public int OrangeScore => orangeScore;

        public void StartMatch(MatchMode selectedMode, int limit = 15)
        {
            mode = selectedMode;
            scoreLimit = Mathf.Max(1, limit);
            blueScore = orangeScore = 0;
            ended = false;
            ScoreChanged?.Invoke(TeamId.Blue, blueScore);
            ScoreChanged?.Invoke(TeamId.Orange, orangeScore);
        }

        public void RegisterPoint(TeamId team)
        {
            if (ended || team == TeamId.Neutral) return;

            if (mode == MatchMode.FreeForAll)
            {
                // In FFA, each scoring participant is represented by the Orange score
                // in this small prototype scoreboard.
                orangeScore++;
            }
            else if (team == TeamId.Blue) blueScore++;
            else orangeScore++;

            ScoreChanged?.Invoke(TeamId.Blue, blueScore);
            ScoreChanged?.Invoke(TeamId.Orange, orangeScore);

            if (blueScore >= scoreLimit) End(TeamId.Blue);
            else if (orangeScore >= scoreLimit) End(TeamId.Orange);
        }

        private void End(TeamId winner)
        {
            ended = true;
            MatchEnded?.Invoke(winner);
        }
    }
}
