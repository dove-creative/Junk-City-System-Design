using System;
using UnityEngine;

namespace JunkCity.World
{
    public class Environment : MonoBehaviour
    {
        private static Environment current;

        public static Studio Studio
        {
            get
            {
                if (!current)
                    throw new Exception("Current Environment�� �������� �����Ƿ� ���� Studio�� ������ �� �����ϴ�.");

                return current.studio;
            }
        }
        public static Background Background
        {
            get
            {
                if (!current)
                    throw new Exception("Current Environment�� �������� �����Ƿ� ���� Background�� ������ �� �����ϴ�.");

                return current.background;
            }
        }

        [SerializeField] private Studio studio;
        [SerializeField] private Background background;


        private void Awake()
        {
            current = this;
        }

        private void OnDestroy()
        {
            if (current == this)
                current = null;
        }
    }
}
