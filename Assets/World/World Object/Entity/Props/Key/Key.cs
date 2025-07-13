using System;
using UnityEngine;

namespace JunkCity.World
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Key : MonoBehaviour
    {
        public event Action OnPlayerCollided;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                GetComponent<Rigidbody2D>().simulated = false;
                OnPlayerCollided?.Invoke();
            }
        }
    }
}
