using System;
using UnityEngine;

namespace Project.Scripts
{
    public class CarButtonTrigger : MonoBehaviour
    {
        [SerializeField] private CarButton carButton;
        [SerializeField] private Transform characterFeetMustBeHigherThan;
        
        private void OnTriggerEnter2D(Collider2D other) 
        { 
            TryPress(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TryRelease();
        }

        private void TryPress(Collider2D other)
        {
            if (!other.CompareTag("Player")) 
                return;

            var character = other.GetComponent<CharacterInCarController>();
            if (character == null)
                return;

            float characterFeetY = character.groundCheck.position.y;
            if (character.localVelocity.y <= carButton.characterMaxVelocityY &&
                characterFeetY >= characterFeetMustBeHigherThan.position.y)
            {
                carButton.PressButton();
            }
        }

        private void TryRelease()
        {
            carButton.ReleaseButton();
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(characterFeetMustBeHigherThan.position, 0.1f);
        }
    }
}