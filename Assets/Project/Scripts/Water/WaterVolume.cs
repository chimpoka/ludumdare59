using UnityEngine;

public class WaterVolume : MonoBehaviour
{
    [SerializeField] public Transform surface;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var carInWater = CarInWaterController.instance;

        if (other.attachedRigidbody == carInWater.carBody)
        {
            carInWater.waterVolume = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var carInWater = CarInWaterController.instance;

        if (other.attachedRigidbody == carInWater.carBody)
        {
            carInWater.waterVolume = null;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(surface.position, 1f);
    }
}
