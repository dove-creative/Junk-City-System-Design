using System;
using UnityEngine;

namespace JunkCity.World
{
    public class Environment : MonoBehaviour
    {
        private static Environment current;

        [SerializeField] private bool assignBackgroundToStudio = true;

        public static Studio Studio
        {
            get
            {
                if (!current)
                    throw new Exception("Current Environment가 존재하지 않으므로 현재 Studio를 가져올 수 없습니다.");

                return current.studio;
            }
        }
        public static Background Background
        {
            get
            {
                if (!current)
                    throw new Exception("Current Environment가 존재하지 않으므로 현재 Background를 가져올 수 없습니다.");

                return current.background;
            }
        }

        [SerializeField] private Studio studio;
        [SerializeField] private Background background;


        private void Awake()
        {
            current = this;

            if (assignBackgroundToStudio && studio && background)
                studio.SetBackground(background);
        }

        private void OnDestroy()
        {
            if (current == this)
                current = null;
        }
    }
}
