using System;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] gateClips;

    public float pitchMin = 0.9f;
    public float pitchMax = 1.1f;

    [SerializeField] private GateController gateController;

    [SerializeField] private Sprite spriteDefault;
    [SerializeField] private Sprite spritePressed;
    
    [SerializeField] private SpriteRenderer spriteRenderer;

    [NonSerialized] public int pressedCount;
    public bool isPressed => pressedCount > 0;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPressed == false && !audioSource.isPlaying)
        {
            PlayGateAudio();
        }
        pressedCount++;

        UpdatePressed();

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        pressedCount--;

        UpdatePressed();

        if (isPressed == false)
        {
            audioSource.Stop();
        }
    }

    private void UpdatePressed()
    {
        gateController.isOpening = isPressed;
        spriteRenderer.sprite = isPressed ? spritePressed : spriteDefault;
    }

    void PlayGateAudio()
    {
        AudioClip clip = gateClips[UnityEngine.Random.Range(0, gateClips.Length)];
        audioSource.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
        audioSource.PlayOneShot(clip);

    }
}
