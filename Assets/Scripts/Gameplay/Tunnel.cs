
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField] Transform anchor;
    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            LevelManager.Instance.SpawnNextTunnel(anchor.position);
        }
    }

    bool hasTriggered = false;
}
