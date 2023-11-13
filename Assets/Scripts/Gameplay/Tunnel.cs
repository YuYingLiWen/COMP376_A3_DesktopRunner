
using UnityEngine;

public class Tunnel : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        Debug.Log("Player", this);
    }
}
