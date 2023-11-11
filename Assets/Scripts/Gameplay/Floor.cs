
using UnityEngine;



public sealed class Floor : MonoBehaviour
{
    private void Awake()
    {
        if(Random.Range(0.0f,1.0f) < 0.15f) gameObject.SetActive(false);
    }
}
