
using UnityEngine;

    public class UpCarButtonTrigger : MonoBehaviour
    {
        [SerializeField] private CarButton carButton;
        
        private void OnTriggerEnter2D(Collider2D other) 
        { 
            carButton.PressButton();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            carButton.ReleaseButton();
        }
    }
