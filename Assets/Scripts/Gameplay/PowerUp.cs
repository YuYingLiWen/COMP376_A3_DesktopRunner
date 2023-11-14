using UnityEngine;

public class PowerUp : MonoBehaviour
{
    static Transform player;
    [SerializeField] int score = 25;
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
            player.GetComponent<Player>().AddPoints(score);
        }
    }


    private void Update()
    {
        float up = Mathf.Sin(Time.time + seed);

        transform.position += Vector3.up * up * 0.5f * Time.deltaTime;

        transform.LookAt(player,Vector3.up);
    }
}