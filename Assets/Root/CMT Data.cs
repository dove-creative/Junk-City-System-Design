using UnityEngine;

namespace JunkCity
{
    [CreateAssetMenu(fileName = "CMT Data", menuName = "Scriptable Objects/CMT Data")]
    public class CMTData : ScriptableObject
    {
        [Header("Global Settings")]
        public float Gravity;

        [Header("Horizontal Motion Settings")]
        public float MoveForce;
        public float MaxSpeed;
        public float FrictionRatio;

        [Header("Vertical Motion Settings")]
        public float JumpForce;
    }
}
