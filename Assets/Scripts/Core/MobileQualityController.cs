using UnityEngine;

namespace OfflineFPS.Core
{
    public class MobileQualityController : MonoBehaviour
    {
        [SerializeField] private int targetFrameRate = 60;
        private void Awake()
        {
            Application.targetFrameRate = targetFrameRate;
            QualitySettings.vSyncCount = 0;
            QualitySettings.antiAliasing = 0;
            QualitySettings.shadowResolution = ShadowResolution.Low;
            QualitySettings.shadows = ShadowQuality.HardOnly;
            QualitySettings.shadowDistance = 22f;
            QualitySettings.pixelLightCount = 1;
            QualitySettings.lodBias = 1f;
        }
        public void SetBalanced()
        {
            QualitySettings.SetQualityLevel(Mathf.Clamp(1, 0, QualitySettings.names.Length - 1), true);
            Application.targetFrameRate = 45;
        }
        public void SetPerformance()
        {
            QualitySettings.SetQualityLevel(0, true);
            QualitySettings.shadows = ShadowQuality.Disable;
            Application.targetFrameRate = 60;
        }
        public void SetQuality()
        {
            QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1, true);
            QualitySettings.shadows = ShadowQuality.HardOnly;
            Application.targetFrameRate = 30;
        }
    }
}