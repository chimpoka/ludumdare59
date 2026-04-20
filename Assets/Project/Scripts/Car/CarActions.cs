using System;
using UnityEngine;

public class CarActions : MonoBehaviour
{
    [SerializeField] public WheelImpulseController wheelImpulseController_RearBackwards;
    [SerializeField] public WheelImpulseController wheelImpulseController_RearForward;
    [SerializeField] public WheelImpulseController wheelImpulseController_FrontBackwards;
    [SerializeField] public WheelImpulseController wheelImpulseController_FrontForward;
    [SerializeField] public HelicopterImpulseController helicopterImpulseController;
    [SerializeField] public HookController hookController;
    [SerializeField] public CarInWaterController CarInWaterController;
    
    public void WheelImpulse_RearBackwards()
    {
        wheelImpulseController_RearBackwards.ApplyImpulse();
    }
    
    public void WheelImpulse_RearForward()
    {
        wheelImpulseController_RearForward.ApplyImpulse();
    }
    
    public void WheelImpulse_FrontBackwards()
    {
        wheelImpulseController_FrontBackwards.ApplyImpulse();
    }
    
    public void WheelImpulse_FrontForward()
    {
        wheelImpulseController_FrontForward.ApplyImpulse();
    }
    
    public void HelicopterImpulse()
    {
        helicopterImpulseController.ApplyImpulse();
    }

    public void TryTriggerHook()
    {
        hookController.TryTriggerHook();
    }

    public void WaterPropellerImpulse()
    {
        CarInWaterController.ApplyImpulse();
    }
}
