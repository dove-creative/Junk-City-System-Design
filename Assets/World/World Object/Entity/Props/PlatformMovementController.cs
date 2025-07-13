using UnityEngine;

namespace JunkCity.World
{
    public class PlatformMovementController : MonoBehaviour, IInteractable
    {
        [Header("Platform")]
        [SerializeField] private GameObject platform;
        [SerializeField] private Vector2 from;
        [SerializeField] private Vector2 to;
        [SerializeField] private float speed = 1;

        [Header("Lever")]
        [SerializeField] private GameObject lever;
        [SerializeField] private Color deactivatedLeverColor;
        [SerializeField] private Color activatedLeverColor;

        private bool working = false;
        private bool direction = true;
        private float progress = 0;


        public void Interact(GameObject _)
        {
            if (!platform)
            {
                Debug.LogWarning("대상 Platform이 없기 때문에 Interact 메서드를 실행할 수 없습니다.");
                return;
            }

            if (!working)
            {
                working = true;

                if (lever && lever.TryGetComponent<SpriteRenderer>(out var renderer))
                    renderer.color = activatedLeverColor;
            }
            else
            {
                working = false;

                if (lever && lever.TryGetComponent<SpriteRenderer>(out var renderer))
                    renderer.color = deactivatedLeverColor;
            }
        }

        private void FixedUpdate()
        {
            if (!working || !platform)
                return;

            if (direction)
            {
                progress += speed * Time.deltaTime;

                if (progress >= 1)
                {
                    progress = 1;
                    direction = false;
                }
            }
            else
            {
                progress -= speed * Time.deltaTime;

                if (progress <= 0)
                {
                    progress = 0;
                    direction = true;
                }
            }

            platform.transform.position = Vector2.Lerp(from, to, progress);
        }
    }
}
