
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    [SerializeField] float force = 2.0f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.attachedRigidbody.AddForce(transform.right * force, ForceMode.Impulse);
        }
    }
}
