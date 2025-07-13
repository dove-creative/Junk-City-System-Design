using System;
using UnityEngine;

namespace JunkCity.World
{
    public class Studio : MonoBehaviour
    {
        [Header("Camera Control")]
        [SerializeField] private Vector3 offset = new(0, 1, -10);
        [SerializeField] private float smoothTime = 0.05f;
        [Space]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform target;
        [SerializeField] private Background background;

        [Header("Transition")]
        [SerializeField] private Curtain curtain;

        private Vector3 velocity = Vector3.zero;


        private void Awake()
        {
            if (!mainCamera)
                Debug.LogWarning("Main Camera가 설정되지 않았기 때문에 Studio의 일부 기능을 사용할 수 없습니다.");

            mainCamera.transform.position = GetDesiredPosition();
        }

        public void SetCurtainState(bool close, bool fromFirstState, Action onClosedCallback = null)
        {
            if (!curtain)
            {
                Debug.LogWarning("Curtain이 설정되지 않았기 때문에 Set Curtain 메서드를 사용할 수 없습니다.");
                return;
            }

            if (!close)
            {
                if (fromFirstState)
                    curtain.SetToClose();

                curtain.Open();
            }
            else
            {
                if (fromFirstState)
                    curtain.SetToOpen();

                curtain.Close(onClosedCallback);
            }
        }

        public void SetBackground(Background background) => this.background = background;

        private void LateUpdate()
        {
            if (!mainCamera || !target)
                return;


            mainCamera.transform.position = Vector3.SmoothDamp(
                mainCamera.transform.position, GetDesiredPosition(), ref velocity, smoothTime);
        }

        Vector3 GetDesiredPosition()
        {
            if (!target)
                return offset;


            var desiredPos = target.position + offset;

            if (background)
            {
                var extentY = mainCamera.orthographicSize;
                var extentX = extentY * mainCamera.aspect;

                var limitX = Mathf.Max(background.Size.x / 2 - extentX, 0);
                var limitY = Mathf.Max(background.Size.y / 2 - extentY, 0);
                
                desiredPos.x = Mathf.Clamp(desiredPos.x, -limitX, limitX);
                desiredPos.y = Mathf.Clamp(desiredPos.y, -limitY, limitY);
            }


            return desiredPos;
        }
    }
}
