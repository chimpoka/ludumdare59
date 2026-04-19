using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CarButton : MonoBehaviour
{
    [SerializeField] public float characterMaxVelocityY = -0.1f;
    [SerializeField] public float characterGroundCheckOffset = -0.1f;
    [SerializeField] public UnityEvent onPressed;

    public void PressButton()
    {
        StartCoroutine(OnButtonPressed());
    }

    public IEnumerator OnButtonPressed()
    {
        print($"Pressed: {gameObject.name}");
        onPressed?.Invoke();
        
        yield break;
    }
}
