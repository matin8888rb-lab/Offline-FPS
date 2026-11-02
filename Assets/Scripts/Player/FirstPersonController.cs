using UnityEngine;

namespace OfflineFPS.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private Transform cameraPivot;
        [SerializeField] private float moveSpeed = 4.2f;
        [SerializeField] private float gravity = -18f;
        [SerializeField] private float jumpHeight = 1.1f;
        [SerializeField] private float lookSensitivity = 0.12f;

        private CharacterController character;
        private Vector2 moveInput;
        private Vector3 verticalVelocity;
        private float pitch;
        private bool jumpQueued;
        private Vector2 lookDelta;

        private void Awake()
        {
            character = GetComponent<CharacterController>();
            if (cameraPivot == null && Camera.main != null)
                cameraPivot = Camera.main.transform;
        }

        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            lookDelta += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * 8f;
            if (Input.GetKeyDown(KeyCode.Space)) jumpQueued = true;
#endif
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            if (move.sqrMagnitude > 1f) move.Normalize();
            character.Move(move * moveSpeed * Time.deltaTime);

            if (character.isGrounded && verticalVelocity.y < 0f)
                verticalVelocity.y = -2f;
            if (jumpQueued && character.isGrounded)
                verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpQueued = false;
            verticalVelocity.y += gravity * Time.deltaTime;
            character.Move(verticalVelocity * Time.deltaTime);

            if (lookDelta.sqrMagnitude > 0f)
            {
                transform.Rotate(Vector3.up * lookDelta.x * lookSensitivity);
                pitch = Mathf.Clamp(pitch - lookDelta.y * lookSensitivity, -78f, 78f);
                if (cameraPivot != null)
                    cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
                lookDelta = Vector2.zero;
            }
        }

        public void SetMoveInput(Vector2 value) => moveInput = Vector2.ClampMagnitude(value, 1f);
        public void QueueJump() => jumpQueued = true;
        public void AddLookDelta(Vector2 delta) => lookDelta += delta;
        public void MoveForward() => moveInput = new Vector2(moveInput.x, 1f);
        public void MoveBackward() => moveInput = new Vector2(moveInput.x, -1f);
        public void MoveLeft() => moveInput = new Vector2(-1f, moveInput.y);
        public void MoveRight() => moveInput = new Vector2(1f, moveInput.y);
        public void StopMove() => moveInput = Vector2.zero;
    }
}
