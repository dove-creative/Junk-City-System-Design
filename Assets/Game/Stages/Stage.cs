using System;
using UnityEngine;

namespace JunkCity.Game
{
    public class Stage : MonoBehaviour
    {
        public static Stage Current { get; private set; }
        public event Action<Action> OnStageClosing;

        protected bool isClosing = false;
        

        protected virtual void Awake() => Current = this;

        public virtual void Close(Action callback = null)
        {
            if (isClosing) return;
            isClosing = true;

            OnStageClosing?.Invoke(callback);
        }
    }
}
