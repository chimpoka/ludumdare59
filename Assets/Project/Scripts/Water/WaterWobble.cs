using UnityEngine;

public class WaterWobble : MonoBehaviour
{
    [Header("Movement")]
    public float amplitude = 0.5f;
    public float speed = 1f;

    [Header("Start Direction")]
    public bool startUp = true;

    private Vector3 startPos;
    private float timeOffset;

    void Start()
    {
        startPos = transform.position;
        timeOffset = startUp ? 0f : Mathf.PI;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed + timeOffset) * amplitude;

        transform.position = new Vector3(
            startPos.x,
            startPos.y + y,
            startPos.z
        );
    }
}