using UnityEditor;
using UnityEngine;

namespace JunkCity.World
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Background : MonoBehaviour
    {
        private void Awake() => SetSize();

        public void SetSize()
        {
            var sr = GetComponent<SpriteRenderer>();
            if (!sr.sprite) return;

            sr.drawMode = SpriteDrawMode.Sliced;

            var camHeight = Camera.main.orthographicSize * 2f;
            var camWidth = camHeight * Screen.width / Screen.height;

            var spriteSize = sr.sprite.bounds.size;
            var spriteAspect = spriteSize.x / spriteSize.y;
            
            var targetWidth = camWidth;
            var targetHeight = targetWidth / spriteAspect;

            if (targetHeight < camHeight)
            {
                targetHeight = camHeight;
                targetWidth = targetHeight * spriteAspect;
            }

            sr.size = new Vector2(targetWidth, targetHeight);
        }


        [CustomEditor(typeof(Background))]
        class CubeGenerateButton : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("Set Size"))
                    ((Background)target).SetSize();
            }
        }
    }
}
