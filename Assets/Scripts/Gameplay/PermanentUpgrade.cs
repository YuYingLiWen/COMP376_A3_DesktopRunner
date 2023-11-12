
using UnityEngine;

public class PermanentUpgrade : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;

    float seed;

    [SerializeField] Vector3 rotation;

    private void Start()
    {
        seed = Random.Range(0.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * (speed + seed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) gameObject.SetActive(false);
    }
}
