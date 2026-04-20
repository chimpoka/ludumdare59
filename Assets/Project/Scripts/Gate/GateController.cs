using System;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private Transform movingTransform;
    [SerializeField] private float openSpeed;
    [SerializeField] private float closeSpeed;
    [SerializeField] private float openWheelsSpeed;
    [SerializeField] private float closeWheelsSpeed;
    [SerializeField] private float maxOpenPosition;
    [SerializeField] private float minClosePosition;
    [SerializeField] private List<Transform> gateWheels;
    
    [NonSerialized] public bool isOpening = false;

    private void FixedUpdate()
    {
        var pos = movingTransform.localPosition;
        
        if (isOpening)
        {
            pos.y += openSpeed;
            
            pos.y = Mathf.Min(pos.y, maxOpenPosition);
            
            if (pos.y < maxOpenPosition)
            {
                gateWheels.ForEach(x => x.transform.Rotate(0, 0, openWheelsSpeed));
            }
        }
        else
        {
            pos.y += closeSpeed;
            pos.y = Mathf.Max(pos.y, minClosePosition);

            if (pos.y > minClosePosition)
            {
                gateWheels.ForEach(x => x.transform.Rotate(0, 0, closeWheelsSpeed));
            }
        }
        
        movingTransform.localPosition = pos;
    }
}
