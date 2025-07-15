using System;
using UnityEngine;

namespace JunkCity.World
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Sword : MonoBehaviour
    {
        public event Action<bool> OnAttack;

        private Rigidbody2D rb;
        private Animator animator;
        private bool isAttacking = false;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            rb.simulated = false;
        }

        public void Attack()
        {
            if (isAttacking) return;
            isAttacking = true;

            rb.simulated = true;
            animator.SetTrigger("Attack");

            OnAttack?.Invoke(true);
        }
        // 이 메서드는 애니메이션이 종료될 때 호출됩니다.
        internal void EndAttack()
        {
            OnAttack?.Invoke(false);
            rb.simulated = false;

            isAttacking = false;
        }
    }
}
