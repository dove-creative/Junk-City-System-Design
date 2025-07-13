using JunkCity.World;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace JunkCity
{
    public class IngameStage : MonoBehaviour
    {
        [Header("Real World")]
        [SerializeField] private GameObject realWorld;
        [SerializeField] private Intercator pc;

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
            pc.OnInteract += _ => SetWorld(true);

            virtualWorld.SetActive(false);
            key.OnPlayerCollided += () =>
            {
                SetWorld(false);
                pc.enabled = false;
            };

            player.transform.position = initialPlayerPosition;

            Environment.Studio.SetCurtainState(false);
        }


        void SetWorld(bool toVirtualWorld)
        {
            if (settingWorld) return;
            settingWorld = true;

            beforeWorldPlayerPosition = player.transform.position;

            Environment.Studio.SetCurtainState(true, () =>
            {
                Environment.Background.Color =
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
                Environment.Studio.SetCurtainState(false);
            });
        }
    }
}
