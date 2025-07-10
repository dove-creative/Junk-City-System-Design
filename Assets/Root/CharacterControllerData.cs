using UnityEngine;

namespace JunkCity
{
    [CreateAssetMenu(fileName = "CharacterControlData", menuName = "Scriptable Objects/CharacterControlData")]
    public class PhysicsData : ScriptableObject
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
