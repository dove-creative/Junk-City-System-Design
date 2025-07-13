using UnityEditor;
using UnityEngine;

namespace JunkCity.Tools
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class ColliderSizeHandler : MonoBehaviour
    {
        [SerializeField] private bool setColliderSizeOnStart = false;


        private void Awake()
        {
            if (setColliderSizeOnStart)
                SetColliderSize();
        }

        public void SetColliderSize()
        {
            var sr = GetComponent<SpriteRenderer>();
            if (!sr.sprite) return;

            var col = sr.GetComponent<BoxCollider2D>();
            col.size = sr.bounds.size;
        }


        [CustomEditor(typeof(ColliderSizeHandler))]
        class CubeGenerateButton : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("Set Collider Size"))
                    ((ColliderSizeHandler)target).SetColliderSize();
            }
        }
    }
}
