using UnityEngine;
using System.Collections;

public class LightningVolume : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var car = CarActions.instance;

        if (other.attachedRigidbody == car.carBody)
        {
            car.lightningRodController.lightningVolume = this;
            StartCoroutine(PlayThunder());

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var car = CarActions.instance;

        if (other.attachedRigidbody == car.carBody)
        {
            car.lightningRodController.lightningVolume = null;
            StopAllCoroutines();
            audioSource.Stop();
        }
    }

    IEnumerator PlayThunder()
    {
        while (true)
        {
            audioSource.pitch = Random.Range(0.9f, 1.2f);
            audioSource.Play();

            yield return new WaitUntil(() => !audioSource.isPlaying);
        }
    }

}
