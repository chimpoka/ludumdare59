using UnityEngine;

public class OutroTriggerVolume : MonoBehaviour
{
    [SerializeField] public Animator outroAnimator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        CarActions.instance.carBody.rotation = 0;
        CarActions.instance.carBody.transform.rotation = Quaternion.identity;
        
        CarActions.instance.carBody.simulated = false;
        
        outroAnimator.enabled = true;
        outroAnimator.SetTrigger("PlayOutro");
    }
}
