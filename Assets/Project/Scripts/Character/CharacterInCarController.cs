using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterInCarController : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference fallAction;
    
    public Rigidbody2D carBody;
    
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float fallForce = 3f;
    public float fallOffsetY = -0.5f;
    public float gravity = 0.1f;
    
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    public float disableLadderTimeOnFall = 0.1f;

    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public AudioClip[] jumpClips;

    public float pitchMin = 0.9f;
    public float pitchMax = 1.1f;

    private Rigidbody2D rb;
    private bool isGrounded;
    
    [NonSerialized] public float horizontalInput;
    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool fallInput;
    //private float verticalInput;
    
    // private bool isTouchingWall_Left;
    // private bool isTouchingWall_Right;

    public Vector2 localZeroVelocity => carBody.linearVelocity;
    public Vector2 localVelocity => rb.linearVelocity - localZeroVelocity;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void OnEnable()
    {
        moveAction?.action.Enable();
        jumpAction?.action.Enable();
        fallAction?.action.Enable();
    }

    private void OnDisable()
    {
        moveAction?.action.Disable();
        jumpAction?.action.Disable();
        fallAction?.action.Disable();
    }

    void Update()
    {
        horizontalInput = moveAction?.action.ReadValue<Vector2>().x ?? 0f;
        // verticalInput = moveAction?.action.ReadValue<Vector2>().y ?? 0f;
        
        if (jumpAction?.action.triggered == true)
        {
            jumpInput = true;
            PlayJumpAudio();
        }    
            
        
        if (fallAction?.action.triggered == true)
            fallInput = true;

        if(horizontalInput != 0f && !audioSource.isPlaying && isGrounded)
        {
            PlayFootstep();
        }
    }

    void PlayFootstep()
    {
        AudioClip clip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
        audioSource.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
        audioSource.PlayOneShot(clip);
    }

    void PlayJumpAudio()
    {
        AudioClip clip = jumpClips[UnityEngine.Random.Range(0, jumpClips.Length)];
        audioSource.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
        audioSource.PlayOneShot(clip);
    }

    void FixedUpdate()
    {
        // Velocity
        Vector2 velocity = rb.linearVelocity;
        
        velocity.x = horizontalInput * moveSpeed;
        velocity.x += carBody.linearVelocity.x;
        
        // Jump
        Collider2D feetOverlapObject = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        isGrounded = feetOverlapObject != null;
        if (jumpInput && isGrounded)
        {
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            velocity.y = jumpForce;
        }
        
        // Fall
        if (fallInput && isGrounded)
        {
            if (feetOverlapObject.CompareTag("Ladder"))
            {
                var pos = transform.position;
                pos.y += fallOffsetY;
                transform.position = pos;
                
                velocity.y = fallForce;
                
                feetOverlapObject.gameObject.SetActive(false);
                StartCoroutine(WaitAndEnableObject(feetOverlapObject.gameObject, disableLadderTimeOnFall));
            }
        }
        
        // Gravity 
        velocity.y -= gravity;
            
        // Result
        rb.linearVelocity = velocity;

        jumpInput = false;
        fallInput = false;
    }

    private IEnumerator WaitAndEnableObject(GameObject InObject, float InTime)
    {
        yield return new WaitForSeconds(InTime);
        
        InObject.SetActive(true);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}