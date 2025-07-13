using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JunkCity.World
{
    [RequireComponent(typeof(CharacterDriver), typeof(PlayerInput))]
    public class UserDriver : MonoBehaviour
    {
        private CharacterDriver driver;
        private List<IInteractable> interactables;


        private void Awake()
        {
            driver = GetComponent<CharacterDriver>();
            interactables = new();
        }

        internal void OnMove(InputValue value)
        {
            driver.MoveHorizontal(value.Get<Vector2>().x);
        }
        internal void OnJump(InputValue value)
        {
            driver.Jump(value.Get<float>());
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.gameObject.TryGetComponent<IInteractable>(out var target))
                return;

            interactables.Add(target);
        }
        private void OnTriggerExit2D(Collider2D collider)
        {
            if (!collider.gameObject.TryGetComponent<IInteractable>(out var target))
                return;

            interactables.Remove(target);
        }
        internal void OnInteract(InputValue _)
        {
            foreach (var entity in interactables)
                entity.Interact(gameObject);
        }
    }
}
