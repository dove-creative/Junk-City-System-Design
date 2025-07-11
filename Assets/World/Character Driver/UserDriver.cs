using UnityEngine;
using UnityEngine.InputSystem;

namespace JunkCity.World
{
    [RequireComponent(typeof(CharacterDriver), typeof(PlayerInput))]
    public class UserDriver : MonoBehaviour
    {
        private CharacterDriver driver;


        private void Awake()
        {
            driver = GetComponent<CharacterDriver>();    
        }

        internal void OnMove(InputValue value)
        {
            driver.MoveHorizontal(value.Get<Vector2>().x);
        }
        internal void OnJump(InputValue value)
        {
            driver.Jump(value.Get<float>());
        }

        internal void OnInteract(InputValue _)
        {
            foreach (var entity in driver.CurrentInteractableEntities)
                entity.Interact();
        }
    }
}
