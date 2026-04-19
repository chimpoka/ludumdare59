using UnityEngine;

public class CarWire : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [SerializeField] private float glowIntensity;

    private void Awake()
    {
        spriteRenderer.material = new Material(spriteRenderer.material);
    }
    
    private void LateUpdate()
    {
        spriteRenderer.material.SetFloat("_GlowIntensity", glowIntensity);
    }

    public void TriggerSignal()
    {
        animator.SetTrigger("Signal");
    }
}
