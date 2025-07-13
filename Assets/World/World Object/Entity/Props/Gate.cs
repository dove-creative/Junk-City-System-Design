using System;
using UnityEngine;

namespace JunkCity.World
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class Gate : MonoBehaviour, IInteractable
    {
        public event Action OnInteract;
        [SerializeField] private Color deactivatedColor;

        private SpriteRenderer spriteRenderer;
        private new Collider2D collider;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = deactivatedColor;

            collider = GetComponent<Collider2D>();
            collider.enabled = false;
        }

        public void Activate()
        {
            spriteRenderer.color = Color.white;
            collider.enabled = true;
        }

        public void Interact(GameObject target)
        {
            if (target.CompareTag("Player"))
                OnInteract?.Invoke();
        }
    }
}
