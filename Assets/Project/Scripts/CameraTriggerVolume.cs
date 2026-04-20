using Unity.Cinemachine;
using UnityEngine;

public class CameraTriggerVolume : MonoBehaviour
{
    [SerializeField] private CinemachineCamera virtualCamera;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        virtualCamera.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        virtualCamera.gameObject.SetActive(false);
    }
}
