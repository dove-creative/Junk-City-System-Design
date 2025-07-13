using System.Collections.Generic;
using UnityEngine;

namespace JunkCity.World
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Sword : MonoBehaviour, IWeapon
    {
        public float AttackPower
        {
            get => attackPower;
            set => attackPower = value;
        }
        [SerializeField] private float attackPower = 50;

        private Rigidbody2D rb;
        private Animator animator;
        private bool isAttacking = false;
        private List<IDamageable> targets;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            rb.simulated = false;
            targets = new();
        }

        public void Attack()
        {
            if (isAttacking) return;
            isAttacking = true;

            rb.simulated = true;
            animator.SetTrigger("Attack");
        }
        // �� �޼���� �ִϸ��̼��� ����� �� ȣ��˴ϴ�.
        internal void EndAttack()
        {
            targets.Clear();
            rb.simulated = false;

            isAttacking = false;
        }

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
