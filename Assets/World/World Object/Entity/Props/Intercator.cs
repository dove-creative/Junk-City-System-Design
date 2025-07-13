using System;
using UnityEngine;

namespace JunkCity.World
{
    public class Intercator : MonoBehaviour, IInteractable
    {
        public event Action<object> OnInteract;

        public void Interact(object target)
        {
            if (!enabled)
                return;

            OnInteract?.Invoke(target);
        }
    }
}
