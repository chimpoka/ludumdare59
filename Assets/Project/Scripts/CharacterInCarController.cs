using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterInCarController : MonoBehaviour
{
    [Header("Input System")]
    [Tooltip("Действие движения (Value → Vector2)")]
    public InputActionReference moveAction;
    [Tooltip("Действие прыжка (Button)")]
    public InputActionReference jumpAction;

    [Header("Физика машины")]
    [Tooltip("Rigidbody2D кузова машины (для компенсации скольжения)")]
    public Rigidbody2D carBody;

    [Header("Параметры")]
    public float moveSpeed = 5f;
    [Range(0f, 1f)] public float airControl = 0.4f; // Управление в воздухе
    public float jumpForce = 7f;

    [Header("Проверка земли")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    private bool jumpInput;
    
    // 1. Добавь в начало класса (после переменных)
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
        // 1. Проверка контакта с полом/стенами комнаты
        isGrounded = true;// Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // 2. Расчёт целевой скорости по X
        float speed = isGrounded ? moveSpeed : moveSpeed * airControl;
        float targetX = horizontalInput * speed;

        // 3. Компенсация движения машины (персонаж не будет "отставать" при разгоне/торможении)
        if (isGrounded && carBody != null)
            targetX += carBody.linearVelocity.x;

        // 4. Применяем скорость. Y не трогаем (гравитация и прыжки работают сами)
        Vector2 vel = rb.linearVelocity;
        vel.x = targetX;
        rb.linearVelocity = vel;

        if (isTouchingWall && carBody != null)
        {
            // Заменяем скорость персонажа на скорость машины по X.
            // Это физически "разрывает" эффект толкания: персонаж просто скользит вместе со стеной.
            rb.linearVelocity = new Vector2(carBody.linearVelocity.x, rb.linearVelocity.y);
        }
        
        // 5. Прыжок
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        if (jumpInput && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpInput = false;
        }

        // 6. Жёсткая блокировка вращения (персонаж всегда горизонтален)
        //rb.freezeRotation = true;
        //transform.rotation = Quaternion.identity;
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
    
    // Визуализация радиуса проверки земли в Scene
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}