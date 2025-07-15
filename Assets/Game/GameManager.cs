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
                        // 이미 종료 중이므로 이중 종료 방지
                        await SetStage(StageName.Lobby, true);
                    };
                }
            }
        }
        /// <summary>
        /// UI 호출용 메서드
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

            throw new ArgumentException($"유효하지 않은 Stage Name({stageName})을 입력했습니다.", nameof(stageName));
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
