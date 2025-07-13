using System;
using UnityEngine;

namespace JunkCity.World
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Key : MonoBehaviour
    {
        public bool Activated { get; private set; }
        public event Action OnPlayerCollided;

        [SerializeField] private Color deactivatedColor;

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();

            spriteRenderer.color = deactivatedColor;
            rb.simulated = false;

            Activated = false;
        }

        public void Activate()
        {
            if (Activated)
                return;

            rb.simulated = true;
            spriteRenderer.color = Color.white;

            Activated = true;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!Activated)
                return;

            if (collider.gameObject.CompareTag("Player"))
            {
                rb.simulated = true;
                OnPlayerCollided?.Invoke();
            }
        }
    }
}
