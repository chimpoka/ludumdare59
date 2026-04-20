using System;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{ 
    [SerializeField] private GateController gateController;

    [SerializeField] private Sprite spriteDefault;
    [SerializeField] private Sprite spritePressed;
    
    [SerializeField] private SpriteRenderer spriteRenderer;

    [NonSerialized] public bool isPressed;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        spriteRenderer.sprite = spritePressed;
        
        isPressed = true;
        gateController.isOpening = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        spriteRenderer.sprite = spriteDefault;
        
        isPressed = false;
        gateController.isOpening = false;
    }
}
