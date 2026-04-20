using System;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private Transform movingTransform;
    [SerializeField] private float openSpeed;
    [SerializeField] private float closeSpeed;
    [SerializeField] private float maxOpenPosition;
    [SerializeField] private float minClosePosition;
    
    [NonSerialized] public bool isOpening;

    private void FixedUpdate()
    {
        var pos = movingTransform.position;
        
        if (isOpening)
        {
            pos.y += openSpeed;
            pos.y = Mathf.Max(pos.y, maxOpenPosition);
        }
        else
        {
            pos.y -= closeSpeed;
            pos.y = Mathf.Min(pos.y, minClosePosition);
        }
        
        movingTransform.position = pos;
    }
}
