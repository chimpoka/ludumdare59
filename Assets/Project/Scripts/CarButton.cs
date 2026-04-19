using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CarButton : MonoBehaviour
{
    [SerializeField] public float characterMaxVelocityY = -0.1f;
    [SerializeField] public float characterGroundCheckOffset = -0.1f;
    [SerializeField] public UnityEvent onPressed;

    [SerializeField] public Sprite spriteDefault;
    [SerializeField] public Sprite spritePressed;
    
    [SerializeField] public SpriteRenderer spriteRenderer;

    public void PressButton()
    {
        StartCoroutine(OnButtonPressed());
    }

    public void ReleaseButton()
    {
        StartCoroutine(OnButtonReleased());
    }

    public IEnumerator OnButtonPressed()
    {
        print($"Pressed: {gameObject.name}");

        spriteRenderer.sprite = spritePressed;
        
        onPressed?.Invoke();
        
        yield break;
    }
    
    public IEnumerator OnButtonReleased()
    {
        print($"Released: {gameObject.name}");

        spriteRenderer.sprite = spriteDefault;
        
        yield break;
    }
}
