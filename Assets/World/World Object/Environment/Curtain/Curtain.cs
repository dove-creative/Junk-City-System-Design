using System;
using UnityEngine;

namespace JunkCity.World
{
    [RequireComponent(typeof(Animator))]
    public class Curtain : MonoBehaviour
    {
        private Animator animator;
        private Action onClosedCallback;

        const string AnimName = "Curtain Transition";


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Open()
        {
            animator.SetFloat("Speed", 1);
            animator.Play(AnimName, -1, 0);
        }
        
        public void Close(Action onClosedCallback = null)
        {
            this.onClosedCallback = onClosedCallback;
            
            animator.SetFloat("Speed", -1);
            animator.Play(AnimName, -1, 1);
        }

        internal void OnClose()
        {
            if (animator.GetFloat("Speed") >= 0)
                return;

            var _onClosedCallback = onClosedCallback;
            onClosedCallback = null;

            _onClosedCallback?.Invoke();
        }
    }
}
