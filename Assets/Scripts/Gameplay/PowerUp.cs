
using System.Collections;

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

        audioS = GetComponent<AudioSource>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Player>().AddPoints(score);
            audioS.PlayOneShot(gatherSFX);

            StartCoroutine(DisableRoutine());
        }
    }

    private void Update()
    {
        float up = Mathf.Sin(Time.time + seed);

        transform.position += Vector3.up * up * 0.5f * Time.deltaTime;

        transform.LookAt(player,Vector3.up);
    }

    IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(gatherSFX.length);
        gameObject.SetActive(false);
    }

    AudioSource audioS;
    [SerializeField] AudioClip gatherSFX;
}