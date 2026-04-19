using UnityEngine;

public class HookController : MonoBehaviour
{
    public Transform hookPoint;
    public Animator animator;
    public LayerMask Hookable;

    private Rigidbody2D carriedObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("isHooking", true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    // ���������� �� ��������!
    public void Pick()
    {
        Collider2D hit = Physics2D.OverlapCircle(hookPoint.position, 1f, Hookable);
        animator.SetBool("isHooking", false);
        Debug.Log("Хук");

        if (hit != null && hit.attachedRigidbody != null)
        {

            HookableObject hookable = hit.GetComponent<HookableObject>();

            if (hookable != null)
            {
                if (hookable.type == HookType.Carry)
                {
                    animator.SetTrigger("Lift"); // анимация подъёма
                }
                else if (hookable.type == HookType.Drag)
                {
                    animator.SetTrigger("Drag"); // другая анимация
                }
            }

            carriedObject = hit.attachedRigidbody;

            carriedObject.linearVelocity = Vector2.zero;
            carriedObject.bodyType = RigidbodyType2D.Kinematic;

            carriedObject.transform.parent = hookPoint;
            carriedObject.transform.localPosition = Vector3.zero;
        }

    }

    public void Drop()
    {
        if (carriedObject != null)
        {
            carriedObject.transform.parent = null;
            carriedObject.bodyType = RigidbodyType2D.Dynamic;

            carriedObject = null;
        }

        animator.SetBool("isHooking", false);
    }
}