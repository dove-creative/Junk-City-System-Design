using UnityEngine;

namespace JunkCity.Infrastructure
{
    [RequireComponent(typeof(CharacterController))]
    public class AssignCharacterControllerData : MonoBehaviour
    {
        private void Start()
        {
            var client = GetComponent<CharacterController>();
            var data = Configuraions.CharacterControllerData;

            client.Gravity = data.Gravity;
            client.MoveForce = data.MoveForce;
            client.MaxSpeed = data.MaxSpeed;
            client.FrictionRatio = data.FrictionRatio;
            client.JumpForce = data.JumpForce;
        }
    }
}
