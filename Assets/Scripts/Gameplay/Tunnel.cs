
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    LevelManager levelManager;
    [SerializeField] Transform anchor;

    private void Awake()
    {
        if (!levelManager) levelManager = FindAnyObjectByType<LevelManager>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            levelManager.SpawnNextTunnel(anchor.position);
        }
    }

    bool hasTriggered = false;
}
