using UnityEngine;

namespace JunkCity.World
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Background : MonoBehaviour
    {
        public Vector2 Size { get; private set; }
        public Color Color
        {
            get => spriteRenderer.color;
            set => spriteRenderer.color = value;
        }

        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer.sprite)
                Size = spriteRenderer.sprite.bounds.size;
            else
                Size = Vector2.zero;
        }
    }
}

