using UnityEngine;

namespace JunkCity.World
{
    [RequireComponent(typeof(CharacterDriver))]
    public class CharacterDriverDataAssigner : MonoBehaviour
    {
        private void Start()
        {
            var client = GetComponent<CharacterDriver>();
            var data = Configuraions.CharacterDriverData;

            client.Gravity = data.Gravity;
            client.MoveForce = data.MoveForce;
            client.MaxSpeed = data.MaxSpeed;
            client.FrictionRatio = data.FrictionRatio;
            client.JumpForce = data.JumpForce;
        }
    }
}
