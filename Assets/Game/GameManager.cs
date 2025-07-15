using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JunkCity.Game
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;


        private async void Awake()
        {
            if (instance != null)
                return;

            instance = this;
            DontDestroyOnLoad(gameObject);

            await SetStage(StageName.Lobby, true);
        }

        public static async Task SetStage(StageName stage, bool directSet = false)
        {
            if (!directSet && Stage.Current)
                Stage.Current.Close(async () => await LoadStage(stage));
            else
                await LoadStage(stage);


            static async Task LoadStage(StageName stage)
            {
                await SceneManager.LoadSceneAsync(stage.ToString());

                if (stage == StageName.Lobby)
                {

                }
                else if (stage == StageName.Ingame)
                {
                    Stage.Current.OnStageClosing += async _ =>
                    {
                        // �̹� ���� ���̹Ƿ� ���� ���� ����
                        await SetStage(StageName.Lobby, true);
                    };
                }
            }
        }
        /// <summary>
        /// UI ȣ��� �޼���
        /// </summary>
        internal static async void SetStage(string stageName)
        {
            foreach (StageName stage in Enum.GetValues(typeof(StageName)))
            {
                if (stage.ToString() == stageName)
                {
                    await SetStage(stage, false);
                    return;
                }
            }

            throw new ArgumentException($"��ȿ���� ���� Stage Name({stageName})�� �Է��߽��ϴ�.", nameof(stageName));
        }

        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
