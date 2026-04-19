using System;
using UnityEngine;

namespace Project.Scripts
{
    public class CarButtonTrigger : MonoBehaviour
    {
        [SerializeField] private CarButton carButton;
        
        private void OnTriggerEnter2D(Collider2D other) 
        { 
            TryPress(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (Application.isPlaying)
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
            float characterFeetMustBeHigherThan = transform.position.y + carButton.characterGroundCheckOffset;
            if (character.localVelocity.y <= carButton.characterMaxVelocityY &&
                characterFeetY >= characterFeetMustBeHigherThan)
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
            var characterFeetMustBeHigherThan = transform.position;
            characterFeetMustBeHigherThan.y += carButton.characterGroundCheckOffset;
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(characterFeetMustBeHigherThan, 0.1f);
        }
    }
}