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
    
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    private bool jumpInput;
    
    private bool isTouchingWall;

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
        if (jumpAction?.action.triggered == true)
            jumpInput = true;
    }

    void FixedUpdate()
    {
        float targetX = horizontalInput * moveSpeed;
        targetX += carBody.linearVelocity.x;
        
        Vector2 vel = rb.linearVelocity;
        vel.x = targetX;
        rb.linearVelocity = vel;
        
        // if (isTouchingWall && carBody != null)
        // {
        //     rb.linearVelocity = new Vector2(carBody.linearVelocity.x, rb.linearVelocity.y);
        // }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        if (jumpInput && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpInput = false;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        return;
        
        isTouchingWall = false; // Сбрасываем каждый кадр физического контакта

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // |normal.x| > 0.8 означает, что поверхность наклонена менее чем на ~37° от вертикали
            // Это надёжно определяет боковые стены, даже если они под небольшим углом
            if (Mathf.Abs(contact.normal.x) > 0.8f)
            {
                isTouchingWall = true;
                break;
            }
        }
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