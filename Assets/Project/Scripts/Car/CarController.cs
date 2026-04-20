using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] public Rigidbody2D carBody;
    [SerializeField] private float carMaxAngle = 40;

    [SerializeField] public bool isEnabled = true;

    private void FixedUpdate()
    {
        if (!isEnabled)
            return;
        
        float current = carBody.rotation;
        float normalized = Mathf.DeltaAngle(0f, current);
        float clamped = Mathf.Clamp(normalized, -carMaxAngle, carMaxAngle);
        
        carBody.rotation = clamped;
    }
}
