using UnityEngine;
using UnityEngine.InputSystem;

namespace JunkCity.Infrastructure
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
    public class UserController : MonoBehaviour
    {
        private CharacterController controller;


        private void Awake()
        {
            controller = GetComponent<CharacterController>();    
        }

        internal void OnMove(InputValue value)
        {
            controller.MoveHorizontal(value.Get<Vector2>().x);
        }
        internal void OnJump(InputValue value)
        {
            controller.Jump(value.Get<float>());
        }
    }
}
