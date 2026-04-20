using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] public Rigidbody2D carBody;
    [SerializeField] private float carMaxAngle = 40;
    //[SerializeField] private float adjustRotationSpeed = 1;

    public static CarController instance;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        float current = carBody.rotation;
        float normalized = Mathf.DeltaAngle(0f, current);
        float clamped = Mathf.Clamp(normalized, -carMaxAngle, carMaxAngle);
        
        carBody.rotation = clamped;
        
        /*
        
        float z = transform.eulerAngles.z;
        if (z > 180f) z -= 360f;
        z = Mathf.Clamp(z, -carMaxAngle, carMaxAngle);
        
        if (z > carMaxAngle)
        {
           // carBody.rotation = Quaternion.Lerp(carBody.rotation, Quaternion.Euler(0, 0, z), adjustRotationSpeed);
            carBody.rotation = Quaternion.Euler(0, 0, z);
        }
        else if (z < -carMaxAngle)
        {
           // carBody.rotation = Quaternion.Lerp(carBody.rotation, Quaternion.Euler(0, 0, -z), adjustRotationSpeed);
            carBody.rotation = Quaternion.Euler(0, 0, -z);
        }
        
        */
    }
}
