using UnityEngine;

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
    }
}
