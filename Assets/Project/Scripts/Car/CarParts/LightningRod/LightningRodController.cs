using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightningRodController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public InputActionReference impulseButtonAction;
    [SerializeField] public Animator animator;
    [SerializeField] private float glowIntensity;
    [SerializeField] private HelicopterImpulseController impulseController;

    [NonSerialized] public LightningVolume lightningVolume;

    public bool isInLightningVolume => lightningVolume != null;
    
    private void Awake()
    {
        spriteRenderer.material = new Material(spriteRenderer.material);
    }
    
    private void LateUpdate()
    {
        spriteRenderer.material.SetFloat("_GlowIntensity", glowIntensity);
    }

    private void OnEnable()
    {
        if (impulseButtonAction != null)
        {
            impulseButtonAction.action.Enable();
            impulseButtonAction.action.started += TryTriggerLightningRodAction;
        }
    }

    private void OnDisable()
    {
        if (impulseButtonAction != null)
        {
            impulseButtonAction.action.started -= TryTriggerLightningRodAction;
            impulseButtonAction.action.Disable();
        }
    }
    
    private void TryTriggerLightningRodAction(InputAction.CallbackContext ctx = default)
    {
        TryTriggerLightningRod();
    }

    public bool TryTriggerLightningRod()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            
        // If any animation in progress
        if (stateInfo.normalizedTime < 1.0f)
            return false;
            
        if (isInLightningVolume)
        {
            animator.SetTrigger("ExtendAndCatchLightning");
        }
        else
        {
            animator.SetTrigger("ExtendEmpty");
        }

        return true;
    }

    public void LightningHit_Event()
    {
        impulseController.ApplyImpulse();
    }
}
