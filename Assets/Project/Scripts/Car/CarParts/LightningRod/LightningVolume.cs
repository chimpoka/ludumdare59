using UnityEngine;

public class LightningVolume : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var car = CarActions.instance;

        if (other.attachedRigidbody == car.carBody)
        {
            car.lightningRodController.lightningVolume = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var car = CarActions.instance;

        if (other.attachedRigidbody == car.carBody)
        {
            car.lightningRodController.lightningVolume = null;
        }
    }
}
