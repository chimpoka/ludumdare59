using System;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterAnimator : MonoBehaviour
{
    [SerializeField] private CarActions car;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private List<Sprite> sprites; // Count = 6
    [SerializeField] private float addHelicopterAnimationSpeedPerImpulse = 100;
    [SerializeField] private float maxHelicopterAnimationSpeed = 100;
    [SerializeField] private float reduceHelicopterAnimationSpeedPerFrame = 1;

    private float currentHelicopterAnimationSpeed = 0;
    private float animationTime;
    private int currentSpriteIndex;

    private void OnEnable()
    {
        car.helicopterImpulseController.onImpulseTriggered += OnApplyImpulse;
    }

    private void OnDisable()
    {
        car.helicopterImpulseController.onImpulseTriggered -= OnApplyImpulse;
    }

    private void FixedUpdate()
    {
        currentHelicopterAnimationSpeed -= reduceHelicopterAnimationSpeedPerFrame;
        currentHelicopterAnimationSpeed = Mathf.Clamp(currentHelicopterAnimationSpeed, 0, maxHelicopterAnimationSpeed);

        animationTime += Time.deltaTime;

        float frameTime = 1 / currentHelicopterAnimationSpeed;
        if (animationTime >= frameTime)
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            animationTime = 0;
        }
    }

    private void OnApplyImpulse()
    {
        currentHelicopterAnimationSpeed += addHelicopterAnimationSpeedPerImpulse;
    }
}
