using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HelicopterImpulseController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float upImpulseStrength = 110f;
    public float rightImpulseStrength = 10f;
    
    
    public InputActionReference impulseButtonAction;

    public HelicopterAnimator helicopterAnimator;

    private void OnEnable()
    {
        if (impulseButtonAction != null)
        {
            impulseButtonAction.action.Enable();
            impulseButtonAction.action.started += ApplyImpulse;
        }
    }

    private void OnDisable()
    {
        if (impulseButtonAction != null)
        {
            impulseButtonAction.action.started -= ApplyImpulse;
            impulseButtonAction.action.Disable();
        }
    }
    
    public void ApplyImpulse(InputAction.CallbackContext ctx = default)
    {
        if (rigidBody == null) 
            return;
        
        rigidBody.AddForce(Vector2.up * upImpulseStrength, ForceMode2D.Impulse);
        rigidBody.AddForce(Vector2.right * rightImpulseStrength, ForceMode2D.Impulse);
        
        helicopterAnimator.ApplyImpulseAnimation();
    }
}