using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace JunkCity.World
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterDriver : MonoBehaviour
    {
        public ReadOnlyCollection<IInteractable> CurrentInteractableEntities { get; private set; }
        private List<IInteractable> currentInteractableEntities;

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

            currentInteractableEntities = new();
            CurrentInteractableEntities = currentInteractableEntities.AsReadOnly();
        }


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.gameObject.TryGetComponent<IInteractable>(out var target))
                return;

            currentInteractableEntities.Add(target);
        }
        private void OnTriggerExit2D(Collider2D collider)
        {
            if (!collider.gameObject.TryGetComponent<IInteractable>(out var target))
                return;

            currentInteractableEntities.Remove(target);
        }

        public void MoveHorizontal(float direction)
        {
            this.direction = direction;
        }
        public void Jump(float value)
        {
            if (value > 0 && Mathf.Approximately(rb.linearVelocityY, 0))
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            if (direction != 0)
                rb.AddForce(direction * MoveForce * Vector2.right);
            else if (!Mathf.Approximately(rb.linearVelocityX, 0))
                rb.AddForce(FrictionRatio * -rb.linearVelocityX * Vector2.right);
            else
                rb.linearVelocityX = 0;

            if (Mathf.Abs(rb.linearVelocityX) > MaxSpeed)
                rb.linearVelocityX = Mathf.Sign(rb.linearVelocityX) * MaxSpeed;
        }
    }
}
