using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float powerUpDuration = 10.0f;  
    public float speedMultiplier = 2.0f;   
    public GameObject pickupEffect;        

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

}