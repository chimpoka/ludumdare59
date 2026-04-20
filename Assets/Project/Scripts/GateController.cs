using System;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private Transform movingTransform;
    [SerializeField] private float openSpeed;
    [SerializeField] private float closeSpeed;
    [SerializeField] private float maxOpenPosition;
    [SerializeField] private float minClosePosition;
    
    [NonSerialized] public bool isOpening = false;

    private void FixedUpdate()
    {
        var pos = movingTransform.localPosition;
        
        if (isOpening)
        {
            pos.y += openSpeed;
            pos.y = Mathf.Min(pos.y, maxOpenPosition);
        }
        else
        {
            pos.y -= closeSpeed;
            pos.y = Mathf.Max(pos.y, minClosePosition);
        }
        
        movingTransform.localPosition = pos;
    }
}
