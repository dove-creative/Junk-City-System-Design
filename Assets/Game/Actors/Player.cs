using System;
using UnityEngine;
using JunkCity.World;

namespace JunkCity
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CharacterDriver))]
    public class Player : MonoBehaviour, IDamageable
    {
        public event Action OnDied;

        [SerializeField] private float hp;

        private Rigidbody2D rb;
        private CharacterDriver driver;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            driver = GetComponent<CharacterDriver>();

            OnDied += () => print("플레이어 사망");
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;

            if (hp < 0)
            {
                hp = 0;
                OnDied?.Invoke();
            }
        }
    }
}
