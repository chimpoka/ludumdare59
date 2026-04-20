using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CarButton : MonoBehaviour
{
    [SerializeField] public float characterMaxVelocityY = -0.1f;
    [SerializeField] public UnityEvent onPressed;

    [SerializeField] public Sprite spriteDefault;
    [SerializeField] public Sprite spritePressed;
    
    [SerializeField] public SpriteRenderer spriteRenderer;

    [SerializeField] public CarWire connectedWire;

    public void PressButton()
    {
        spriteRenderer.sprite = spritePressed;
        
        onPressed?.Invoke();
        
        connectedWire.TriggerSignal();
    }

    public void ReleaseButton()
    {
        spriteRenderer.sprite = spriteDefault;
    }
}
