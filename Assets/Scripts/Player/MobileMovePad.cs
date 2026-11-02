using UnityEngine;
using UnityEngine.EventSystems;

namespace OfflineFPS.Player
{
    public class MobileMovePad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private FirstPersonController player;
        [SerializeField, Range(40f, 180f)] private float radius = 110f;
        private RectTransform rectTransform;
        private void Awake() { rectTransform = transform as RectTransform; }
        public void SetPlayer(FirstPersonController target) { player = target; }
        public void OnPointerDown(PointerEventData eventData) { UpdateInput(eventData); }
        public void OnDrag(PointerEventData eventData) { UpdateInput(eventData); }
        public void OnPointerUp(PointerEventData eventData) { if (player != null) player.SetMoveInput(Vector2.zero); }
        private void UpdateInput(PointerEventData eventData)
        {
            if (player == null || rectTransform == null) return;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 local)) return;
            player.SetMoveInput(Vector2.ClampMagnitude(local / radius, 1f));
        }
    }
}