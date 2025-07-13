using System;
using UnityEngine;

namespace JunkCity.World
{
    public class Intercator : MonoBehaviour, IInteractable
    {
        public event Action<GameObject> OnInteract;

        public void Interact(GameObject target)
        {
            if (!enabled)
                return;

            OnInteract?.Invoke(target);
        }
    }
}
