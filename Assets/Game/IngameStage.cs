using System;
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
        [SerializeField] private Key key;

        [Header("Common")]
        [SerializeField] private Transform player;
        [SerializeField] private Vector2 initialPlayerPosition;
        [SerializeField] private PlatformMovementController platformMovementController;

        private bool isFirstTransition = true;
        private Vector2 beforeWorldPlayerPosition;

        private bool settingWorld = false;


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
        }

        private void SetWorld(bool toVirtualWorld)
        {
            if (settingWorld) return;
            settingWorld = true;

            beforeWorldPlayerPosition = player.transform.position;

            World.Environment.Studio.SetCurtainState(true, false, () =>
            {
                World.Environment.Background.Color =
                    toVirtualWorld ? Color.blue : Color.white;

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
                World.Environment.Studio.SetCurtainState(false, false);
            });
        }
    }
}
