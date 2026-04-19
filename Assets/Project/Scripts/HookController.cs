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
        Collider2D hit = Physics2D.OverlapCircle(hookPoint.position, 2f, Hookable);

        if (hit != null && hit.attachedRigidbody != null)
        {
            carriedObject = hit.attachedRigidbody;

            carriedObject.linearVelocity = Vector2.zero;
            carriedObject.bodyType = RigidbodyType2D.Kinematic;

            carriedObject.transform.parent = hookPoint;
            carriedObject.transform.localPosition = Vector3.zero;

            Debug.Log(hit);
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