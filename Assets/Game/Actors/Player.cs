using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using JunkCity.World;

namespace JunkCity
{
    [RequireComponent(typeof(SpriteRenderer), typeof(AccessoryManager), typeof(CharacterDriver))]
    public class Player : MonoBehaviour, IDamageable
    {
        public event Action OnDied;

        [SerializeField] private GameObject[] weaponPrefabs;
        private readonly List<GameObject> ownWeapons = new();

        [SerializeField] private float hp = 100;
        [SerializeField] private float invulnerabilityDuration = 1;
        [SerializeField] private Color invulnerabilityColor;
        private bool isInvulnerable = false;

        private SpriteRenderer spriteRenderer;
        private AccessoryManager accessoryManager;
        private bool alive = true;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            accessoryManager = GetComponent<AccessoryManager>();
        }

        public void SetOwnWeapons()
        {
            if (weaponPrefabs != null)
            {
                foreach (var weaponPrefab in weaponPrefabs)
                {
                    var weapon = Instantiate(weaponPrefab);

                    accessoryManager.Attach(weapon);
                    ownWeapons.Add(weapon);
                }
            }
        }
        public void DetachOwnWeapons()
        {
            foreach (var weapon in ownWeapons)
                accessoryManager.Detach(weapon, true);
        }

        internal void OnAttack(InputValue _)
        {
            if (!alive)
                return;

            foreach (var weapen in accessoryManager.Weapons)
                weapen.Attack();
        }

        public void TakeDamage(float damage)
        {
            if (!alive || isInvulnerable)
                return;

            hp -= damage;

            if (hp <= 0)
            {
                hp = 0;
                alive = false;

                Die();
                return;
            }

            StartCoroutine(MakeInvulnerable());
            IEnumerator MakeInvulnerable()
            {
                isInvulnerable = true;

                var originalColor = spriteRenderer.color;
                spriteRenderer.color = invulnerabilityColor;

                yield return new WaitForSeconds(invulnerabilityDuration);

                spriteRenderer.color = originalColor;
                isInvulnerable = false;
            }
        }

        private void Die()
        {
            GetComponent<CharacterDriver>().enabled = false;

            Debug.Log("플레이어 사망");
            OnDied?.Invoke();
        }
    }
}
