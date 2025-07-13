using System;
using System.Collections.Generic;
using UnityEngine;
using JunkCity.World;

namespace JunkCity
{
    public class IngameStage : MonoBehaviour
    {
        public event Action OnStageCompleted;

        [Header("Real World")]
        [SerializeField] private GameObject realWorld;
        [SerializeField] private Intercator pc;
        [SerializeField] private Gate gate;

        [Header("Virtual World")]
        [SerializeField] private GameObject virtualWorld;
        [SerializeField] private List<Enemy> enemies;
        [SerializeField] private Key key;

        [Header("Common")]
        [SerializeField] private Player player;
        [SerializeField] private Vector2 initialPlayerPosition;
        [SerializeField] private PlatformMovementController platformMovementController;

        private bool isVirtualWorld = false;
        private bool isFirstTransition = true;
        private Vector2 beforeWorldPlayerPosition;

        private bool settingWorld = false;


        private void Awake()
        {
            gate.OnInteract += () => World.Environment.Studio.SetCurtainState(true);

            foreach (var enemy in enemies)
            {
                var _enemy = enemy;
                _enemy.OnDied += () => enemies.Remove(_enemy);
            }
        }

        private void Start()
        {
            realWorld.SetActive(true);
            pc.OnInteract += t =>
            {
                if (t.CompareTag("Player"))
                    SetWorld(true);
            };
            gate.OnInteract += () => OnStageCompleted?.Invoke();

            virtualWorld.SetActive(false);
            key.OnPlayerCollided += () =>
            {
                SetWorld(false);
                pc.enabled = false;

                OnKeyAcquired();
            };

            player.transform.position = initialPlayerPosition;
            World.Environment.Studio.SetCurtainState(false, true);
        }

        private void OnKeyAcquired()
        {
            gate.Activate();
            Debug.Log("게이트 활성화됨");
        }

        private void Update()
        {
            if (isVirtualWorld)
            {
                if (!key.Activated && enemies.Count == 0)
                {
                    key.Activate();
                    Debug.Log("키 활성화됨");
                }
            }
        }

        private void SetWorld(bool toVirtualWorld)
        {
            if (settingWorld) return;
            settingWorld = true;

            beforeWorldPlayerPosition = player.transform.position;

            World.Environment.Studio.SetCurtainState(true, onClosedCallback: () =>
            {
                if (toVirtualWorld)
                {
                    World.Environment.Background.Color = Color.blue;
                    player.SetOwnWeapons();
                }
                else
                {
                    World.Environment.Background.Color = Color.white;
                    player.DetachOwnWeapons();
                }

                isVirtualWorld = toVirtualWorld;
                realWorld.SetActive(!toVirtualWorld);
                virtualWorld.SetActive(toVirtualWorld);

                if (isFirstTransition)
                {
                    isFirstTransition = false;
                    player.transform.position = initialPlayerPosition;
                }
                else
                    player.transform.position = beforeWorldPlayerPosition;

                settingWorld = false;
                World.Environment.Studio.SetCurtainState(false);
            });
        }
    }
}
