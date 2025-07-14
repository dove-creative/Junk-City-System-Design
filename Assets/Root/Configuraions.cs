using UnityEngine;

namespace JunkCity
{
    public class Configuraions : MonoBehaviour
    {
        [SerializeField] CMTData cmtData;
        public static CMTData CMTData => instance.cmtData;

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
