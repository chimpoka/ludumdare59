using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightningRodController : MonoBehaviour
{
    [SerializeField] public InputActionReference impulseButtonAction;
    [SerializeField] public Animator animator;

    [NonSerialized] public LightningVolume lightningVolume;

    public bool isInLightningVolume => lightningVolume != null;
    



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
}
