using UnityEngine;
using UnityEngine.InputSystem;

public class WheelImpulseController : MonoBehaviour
{
    public Rigidbody2D wheelRb;
    public float impulseStrength = 50f; 
    public InputActionReference impulseButtonAction;

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
        if (wheelRb == null) 
            return;
        
        wheelRb.AddTorque(-impulseStrength, ForceMode2D.Impulse);
    }
}