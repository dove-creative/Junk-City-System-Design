using UnityEngine;

namespace JunkCity
{
    public class Configuraions : MonoBehaviour
    {
        [SerializeField] CharacterDriverData characterDriverData;
        public static CharacterDriverData CharacterDriverData => instance.characterDriverData;

        private static Configuraions instance;


        private void Awake()
        {
            if (instance != null)
                return;

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
