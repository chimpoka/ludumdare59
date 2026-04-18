using System;
using UnityEngine;

public class WheelSettings : MonoBehaviour
{
    public WheelImpulseController RearBackwards;
    public WheelImpulseController RearForward;
    public WheelImpulseController FrontBackwards;
    public WheelImpulseController FrontForward;
    
    public float impulseStrength = 50f;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        return;
        RearBackwards.impulseStrength = -impulseStrength;
        RearForward.impulseStrength = impulseStrength;
        FrontBackwards.impulseStrength = -impulseStrength;
        FrontForward.impulseStrength = impulseStrength;
    }
    #endif
}
