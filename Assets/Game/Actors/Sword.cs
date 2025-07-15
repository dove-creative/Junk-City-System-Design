using System.Collections.Generic;
using UnityEngine;

namespace JunkCity.Game
{
    [RequireComponent(typeof(World.Sword))]
    public class Sword : MonoBehaviour, IWeapon
    {
        public float AttackPower
        {
            get => attackPower;
            set => attackPower = value;
        }
        [SerializeField] private float attackPower = 50;

        private World.Sword swordEntity;
        private List<IDamageable> targets;


        private void Awake()
        {
            swordEntity = GetComponent<World.Sword>();
            targets = new();

            swordEntity.OnAttack += starting =>
            {
                if (!starting)
                    targets.Clear();
            };
        }

        public void Attack() => swordEntity.Attack();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var go = collision.gameObject;

            if (!go.CompareTag("Player")
                && go.TryGetComponent<IDamageable>(out var target)
                && !targets.Contains(target))
            {
                target.TakeDamage(attackPower);
                targets.Add(target);
            }
        }
    }
}
