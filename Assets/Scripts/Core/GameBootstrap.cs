using UnityEngine;

namespace OfflineFPS.Core
{
    public class GameBootstrap : MonoBehaviour
    {
        public static GameBootstrap Instance { get; private set; }
        public MatchManager Match { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            Match = GetComponent<MatchManager>();
            if (Match == null) Match = gameObject.AddComponent<MatchManager>();
        }
    }
}
