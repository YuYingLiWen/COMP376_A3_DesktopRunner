
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public sealed class Floor : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player")) rb.isKinematic = false;
    }
}
