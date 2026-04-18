using UnityEngine;
using UnityEngine.InputSystem;

public class HelicopterImpulseController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
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
        if (rigidBody == null) 
            return;
        
        rigidBody.AddForce(Vector2.up * impulseStrength, ForceMode2D.Impulse);
    }
}