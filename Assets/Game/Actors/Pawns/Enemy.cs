using System;
using UnityEngine;

namespace JunkCity.Game
{
    public class Enemy : MonoBehaviour, IPawn, IDamageable
    {
        public event Action OnDamaged;
        public event Action OnDied;

        [SerializeField] private float hp = 100;
        [SerializeField] private float damage = 10;
        private bool alive = true;


        private void Awake()
        {
            OnDied += () =>
            {
                Debug.Log($"{name} »ç¸Á");
                Destroy(gameObject);
            };
        }

        public void TakeDamage(float damage)
        {
            if (!alive)
                return;

            hp -= damage;

            if (hp > 0)
            {
                OnDamaged?.Invoke();
            }
            else
            {
                hp = 0;
                alive = false;

                OnDied?.Invoke();
                return;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!alive)
                return;

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject
                    .GetComponent<Player>()
                    .TakeDamage(damage);
            }
        }

    }
}
