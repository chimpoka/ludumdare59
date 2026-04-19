using System.Collections.Generic;
using UnityEngine;

public class CharacterInCarAnimator : MonoBehaviour
{
    [SerializeField] private CharacterInCarController characterController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private List<Sprite> sprites; // Count = 3
    [SerializeField] private float rotationAnimationSpeed = 0.05f;
    [SerializeField] private float moveAnimationSwitchFrameTime = 0.25f;

    private float moveTime;
    private int currentSpriteIndex = 0;
    private float lastHorizontalDirection;

    private void Update()
    {
        // Move animation
        if (!Mathf.Approximately(characterController.horizontalInput, 0))
        {
            lastHorizontalDirection = characterController.horizontalInput;
            
            moveTime += Time.deltaTime;
            if (moveTime >= moveAnimationSwitchFrameTime)
            {
                currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
                spriteRenderer.sprite = sprites[currentSpriteIndex];
                moveTime = 0;
            }
        }
        else
        {
            moveTime = 0;
            currentSpriteIndex = 0;
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
        
        // Rotate animation
        if (!Mathf.Approximately(characterController.horizontalInput, 0))
        {
            lastHorizontalDirection = characterController.horizontalInput;
        }

        var targetRotation = lastHorizontalDirection >= 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationAnimationSpeed);
    }
}
