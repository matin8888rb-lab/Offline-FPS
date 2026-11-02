using UnityEngine;

namespace OfflineFPS.Core
{
    public static class LocalSave
    {
        private const string BestScoreKey = "offline_fps_best_score";

        public static int BestScore
        {
            get => PlayerPrefs.GetInt(BestScoreKey, 0);
            set
            {
                PlayerPrefs.SetInt(BestScoreKey, Mathf.Max(0, value));
                PlayerPrefs.Save();
            }
        }
    }
}
