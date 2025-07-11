using UnityEngine;

namespace JunkCity.World
{
    public class PlaftormMovementController : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject platform;
        [SerializeField] private Vector2 from;
        [SerializeField] private Vector2 to;
        [SerializeField] private float speed = 1;

        private bool working = false;
        private bool direction = true;
        private float progress = 0;


        public void Interact()
        {
            if (!platform)
            {
                Debug.LogWarning("��� Platform�� ���� ������ Interact �޼��带 ������ �� �����ϴ�.");
                return;
            }

            working = !working;
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
