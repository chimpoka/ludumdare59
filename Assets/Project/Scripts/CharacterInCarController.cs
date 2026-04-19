using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterInCarController : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    
    public Rigidbody2D carBody;
    
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravity = 0.1f;
    
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    
    [NonSerialized] public bool jumpInput;
    [NonSerialized] public float horizontalInput;
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
    }

    private void OnDisable()
    {
        moveAction?.action.Disable();
        jumpAction?.action.Disable();
    }

    void Update()
    {
        horizontalInput = moveAction?.action.ReadValue<Vector2>().x ?? 0f;
        // verticalInput = moveAction?.action.ReadValue<Vector2>().y ?? 0f;
        
        if (jumpAction?.action.triggered == true)
            jumpInput = true;
    }

    void FixedUpdate()
    {
        // Velocity
        Vector2 velocity = rb.linearVelocity;
        
        velocity.x = horizontalInput * moveSpeed;
        velocity.x += carBody.linearVelocity.x;

        // if (isTouchingWall_Left && velocity.x < localZeroVelocity.x || 
        //     isTouchingWall_Right && velocity.x > localZeroVelocity.x)
        //     velocity.x = localZeroVelocity.x;
        
        // Jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        if (jumpInput && isGrounded)
        {
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            velocity.y = jumpForce;
            jumpInput = false;
        }
        
        // Gravity 
        velocity.y -= gravity;
        // if (velocity.y < localZeroVelocity.y)
        //     velocity.y = localZeroVelocity.y;
            
        // Result
        rb.linearVelocity = velocity;

        jumpInput = false;
    }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("RoomWall_Left"))
    //         isTouchingWall_Left = true;
    //     else  if (collision.CompareTag("RoomWall_Right"))
    //         isTouchingWall_Right = true; 
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     isTouchingWall_Left = false;
    //     isTouchingWall_Right = false;
    // }
    
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}