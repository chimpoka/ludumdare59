using UnityEngine;
using UnityEngine.InputSystem;

public class HookController : MonoBehaviour
{
    public Transform hookPoint;
    public Animator animator;
    public LayerMask hookableLayer;
    public InputActionReference hookButtonAction;
    public float grabRadius = 1;
    
    private Rigidbody2D carriedObject;

    public AudioSource audioSource;
    public AudioClip[] clickClips;

    public float pitchMin = 0.9f;
    public float pitchMax = 1.1f;

    private void OnEnable()
    {
        if (hookButtonAction != null)
        {
            hookButtonAction.action.Enable();
            hookButtonAction.action.started += TryTriggerHookAction;
        }
    }

    private void OnDisable()
    {
        if (hookButtonAction != null)
        {
            hookButtonAction.action.started -= TryTriggerHookAction;
            hookButtonAction.action.Disable();
        }
    }

    private void TryTriggerHookAction(InputAction.CallbackContext ctx = default)
    {
        TryTriggerHook();
    }

    public bool TryTriggerHook()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            
        // If any animation in progress
        if (stateInfo.normalizedTime < 1.0f)
            return false;
            
        if (carriedObject == null)
        {
            animator.SetTrigger("DownAndGrab");
        }
        else
        {
            animator.SetTrigger("DropAndUp");
        }
        PlayHookAudio();
        return true;
    }
    
    public void Pick(Collider2D ObjectToPick)
    {
        carriedObject = ObjectToPick.attachedRigidbody;

        carriedObject.linearVelocity = Vector2.zero;
        carriedObject.bodyType = RigidbodyType2D.Kinematic;

        carriedObject.transform.parent = hookPoint;
        carriedObject.transform.localPosition = Vector3.zero;
    }
    
    public void TryPick_AnimEvent()
    {
        Collider2D hit = Physics2D.OverlapCircle(hookPoint.position, grabRadius, hookableLayer);

        if (hit != null && hit.attachedRigidbody != null)
        {
            Pick(hit);
            animator.SetTrigger("UpWithBigObject");
        }
        else
        {
            animator.SetTrigger("UpEmpty");
        }
    }

    public void Drop_AnimEvent()
    {
        if (carriedObject != null)
        {
            carriedObject.transform.parent = null;
            carriedObject.bodyType = RigidbodyType2D.Dynamic;

            carriedObject = null;
        }
    }

    void PlayHookAudio()
    {
        AudioClip clip = clickClips[UnityEngine.Random.Range(0, clickClips.Length)];
        audioSource.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
        audioSource.PlayOneShot(clip);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hookPoint.position, grabRadius);
    }
}