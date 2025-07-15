using UnityEngine;

namespace JunkCity.UI
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private Curtain curtain;
        [SerializeField] private Game.Stage stage;

        private void Start()
        {
            curtain.Open(true);
            stage.OnStageClosing += callback => curtain.Close(onClosed: callback);
        }
    }
}
