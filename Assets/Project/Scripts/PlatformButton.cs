using System;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{ 
    [SerializeField] private GateController gateController;

    [SerializeField] private Sprite spriteDefault;
    [SerializeField] private Sprite spritePressed;
    
    [SerializeField] private SpriteRenderer spriteRenderer;

    [NonSerialized] public int pressedCount;
    public bool isPressed => pressedCount > 0;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        pressedCount++;

        UpdatePressed();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        pressedCount--;

        UpdatePressed();
    }

    private void UpdatePressed()
    {
        gateController.isOpening = isPressed;
        spriteRenderer.sprite = isPressed ? spritePressed : spriteDefault;
    }
}
