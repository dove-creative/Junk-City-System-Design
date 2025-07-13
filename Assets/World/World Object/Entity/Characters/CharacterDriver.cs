using UnityEngine;

namespace JunkCity.World
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))] 
    public class CharacterDriver : MonoBehaviour
    {
        [SerializeField] private bool setCharacterDriverConfiguration = false;


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

        private float direction = 0;
        private Rigidbody2D rb;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            if (setCharacterDriverConfiguration)
            {
                var data = Configuraions.CharacterDriverData;

                Gravity = data.Gravity;
                MoveForce = data.MoveForce;
                MaxSpeed = data.MaxSpeed;
                FrictionRatio = data.FrictionRatio;
                JumpForce = data.JumpForce;
            }
        }

        public void MoveHorizontal(float direction)
        {
            this.direction = direction;
        }
        public void Jump(float value)
        {
            if (value > 0 && Mathf.Approximately(rb.linearVelocityY, 0))
                rb.AddForce(value * JumpForce * Vector2.up, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            if (direction != 0)
                rb.AddForce(direction * MoveForce * Vector2.right);
            else if (Mathf.Abs(rb.linearVelocityX) > 0.1f)
                rb.AddForce(FrictionRatio * -rb.linearVelocityX * Vector2.right);
            else
                rb.linearVelocityX = 0;

            if (Mathf.Abs(rb.linearVelocityX) > MaxSpeed)
                rb.linearVelocityX = Mathf.Sign(rb.linearVelocityX) * MaxSpeed;
        }
    }
}
