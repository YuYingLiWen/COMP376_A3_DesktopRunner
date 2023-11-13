using UnityEngine;

public class PowerUp : MonoBehaviour
{
    static Transform player;

    float seed ;

    private void Awake()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;

        seed = Random.Range(0.0f, 1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        float up = Mathf.Sin(Time.time + seed);

        transform.position += Vector3.up * up * 0.5f * Time.deltaTime;

        transform.LookAt(player,Vector3.up);
    }
}