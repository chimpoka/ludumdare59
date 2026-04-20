using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInWaterController : MonoBehaviour
{
    [SerializeField] public Rigidbody2D carBody;
    [SerializeField] private float gravityUp;
    [SerializeField] private float gravityDown;
    [SerializeField] private float horizontalFriction;
    [SerializeField] private float inWaterImpulseStrength = 50f; 
    [SerializeField] private float onGroundImpulseStrength = 20f; 
    [SerializeField] private InputActionReference impulseButtonAction;
    [SerializeField] private HelicopterAnimator helicopterAnimator;
    
    [NonSerialized] public WaterVolume waterVolume;
  
    public static CarInWaterController instance;
    
    public bool isInWater => waterVolume != null;
    
    private void Awake()
    {
        instance = this;
    }
    
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
        if (carBody == null) 
            return;

        float strength = isInWater ? inWaterImpulseStrength : onGroundImpulseStrength;
        
        carBody.AddForce(Vector2.right * strength, ForceMode2D.Impulse);
        
        helicopterAnimator.ApplyImpulseAnimation();
    }

    private void FixedUpdate()
    {
        if (!isInWater)
            return;

        if (carBody.linearVelocityX < 0)
            carBody.linearVelocityX += horizontalFriction;
        else if (carBody.linearVelocityX > 0)
            carBody.linearVelocityX -= horizontalFriction;

        if (carBody.position.y < waterVolume.surface.position.y)
            carBody.linearVelocityY += gravityUp;
        else if (carBody.position.y > waterVolume.surface.position.y)
            carBody.linearVelocityY -= gravityDown;
    }
}
