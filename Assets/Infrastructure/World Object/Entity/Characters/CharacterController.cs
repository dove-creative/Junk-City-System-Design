using UnityEngine;

namespace JunkCity.Infrastructure
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {
        [Header("Global Motion Settings")]
        public float Gravity
        {
            get => rb.gravityScale;
            set => rb.gravityScale = value;
        }

        [Header("Horizontal Motion Settings")]
        public float MoveForce = 100;
        public float MaxSpeed = 20;
        public float FrictionRatio = 18;

        [Header("Vertical Motion Settings")]
        public float JumpForce = 25;

        private float direction = 0f;
        private Rigidbody2D rb;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void MoveHorizontal(float direction)
        {
            this.direction = direction;
        }
        public void Jump(float value)
        {
            if (value > 0 && Mathf.Approximately(rb.linearVelocityY, 0f))
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            if (direction != 0)
                rb.AddForce(direction * MoveForce * Vector2.right);
            else if (!Mathf.Approximately(rb.linearVelocityX, 0f))
                rb.AddForce(FrictionRatio * -rb.linearVelocityX * Vector2.right);
            else
                rb.linearVelocityX = 0;

            if (Mathf.Abs(rb.linearVelocityX) > MaxSpeed)
                rb.linearVelocityX = Mathf.Sign(rb.linearVelocityX) * MaxSpeed;
        }
    }
}
