using UnityEngine;
using UnityEngine.EventSystems;

namespace OfflineFPS.Player
{
    // Attach to a UI look-area object. Drag this area to rotate the camera.
    public class TouchLook : MonoBehaviour, IDragHandler
    {
        [SerializeField] private FirstPersonController player;
        [SerializeField] private float sensitivity = 1f;

        public void OnDrag(PointerEventData eventData)
        {
            if (player != null)
                player.AddLookDelta(eventData.delta * sensitivity);
        }

        public void SetPlayer(FirstPersonController target) => player = target;
    }
}
