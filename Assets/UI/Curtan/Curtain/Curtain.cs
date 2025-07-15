using System;
using UnityEngine;

namespace JunkCity.UI
{
    [RequireComponent(typeof(Animator))]
    public class Curtain : MonoBehaviour
    {
        private Animator animator;
        private Action onClosed;

        const string AnimName = "Curtain Transition";


        private void Awake()
        {
            animator = GetComponent<Animator>();
            SetToOpen();
        }

        public void SetToOpen()
        {
            animator.Play(AnimName, -0, 1);
            animator.StopPlayback();
        }
        public void SetToClose()
        {
            animator.Play(AnimName, -1, 0);
            animator.StopPlayback();
        }

        public void Open(bool fromClosed = false)
        {
            if (fromClosed)
                SetToOpen();

            animator.SetFloat("Speed", 1);
            animator.Play(AnimName, -1, 0);
        }
        public void Close(bool fromOpened = false, Action onClosed = null)
        {
            if (fromOpened)
                SetToOpen();

            this.onClosed = onClosed;

            animator.SetFloat("Speed", -1);
            animator.Play(AnimName, -1, 1);
        }

        internal void OnClose()
        {
            if (animator.GetFloat("Speed") >= 0)
                return;

            var _onClosedCallback = onClosed;
            onClosed = null;

            _onClosedCallback?.Invoke();
        }
    }
}
