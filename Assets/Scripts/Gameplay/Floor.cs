
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public sealed class Floor : MonoBehaviour
{
    Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player")) rb.useGravity = true;
    }
}
