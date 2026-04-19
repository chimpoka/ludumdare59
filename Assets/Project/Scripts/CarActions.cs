using UnityEngine;

public class CarActions : MonoBehaviour
{
    [SerializeField] private WheelImpulseController wheelImpulseController_RearBackwards;
    [SerializeField] private WheelImpulseController wheelImpulseController_RearForward;
    [SerializeField] private WheelImpulseController wheelImpulseController_FrontBackwards;
    [SerializeField] private WheelImpulseController wheelImpulseController_FrontForward;
    [SerializeField] private HelicopterImpulseController helicopterImpulseController;
    
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
}
