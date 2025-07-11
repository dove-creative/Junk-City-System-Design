using UnityEngine;

namespace JunkCity
{
    [CreateAssetMenu(fileName = "CharacterDriverData", menuName = "Scriptable Objects/Character Driver Data")]
    public class CharacterDriverData : ScriptableObject
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
