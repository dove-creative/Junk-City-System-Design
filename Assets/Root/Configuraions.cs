using UnityEngine;

namespace JunkCity
{
    public class Configuraions : MonoBehaviour
    {
        [SerializeField] PhysicsData characterControllerData;
        public static PhysicsData CharacterControllerData => instance.characterControllerData;

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
