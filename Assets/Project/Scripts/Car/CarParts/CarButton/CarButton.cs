using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CarButton : MonoBehaviour
{
    [SerializeField] public UnityEvent onPressed;

    [SerializeField] public Sprite spriteDefault;
    [SerializeField] public Sprite spritePressed;
    
    [SerializeField] public SpriteRenderer spriteRenderer;

    [SerializeField] public CarWire connectedWire;

    public AudioSource audioSource;
    public AudioClip[] clickClips;

    public float pitchMin = 0.9f;
    public float pitchMax = 1.1f;

    public void PressButton()
    {
        spriteRenderer.sprite = spritePressed;
        
        onPressed?.Invoke();
        
        connectedWire.TriggerSignal();

        PlayButton();
    }

    void PlayButton()
    {
        AudioClip clip = clickClips[UnityEngine.Random.Range(0, clickClips.Length)];
        audioSource.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
        audioSource.PlayOneShot(clip);
    }

    public void ReleaseButton()
    {
        spriteRenderer.sprite = spriteDefault;
    }
}
