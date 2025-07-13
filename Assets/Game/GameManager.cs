using UnityEngine;
using UnityEngine.SceneManagement;

namespace JunkCity
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;


        private void Awake()
        {
            if (instance != null)
                return;

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        protected static void SetStage(StageName scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }
}
