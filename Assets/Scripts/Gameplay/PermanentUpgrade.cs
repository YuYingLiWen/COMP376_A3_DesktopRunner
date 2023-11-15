using System.Collections;
using UnityEngine;

public class PermanentUpgrade : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] int score = 50;
    float seed;

    [SerializeField] Vector3 rotation;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }

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
        if (other.CompareTag("Player"))
        {
            audioS.PlayOneShot(gatherSFX);
            other.GetComponent<Player>().AddPoints(score);

            StartCoroutine(DisableRoutine());

        }
    }

    IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(gatherSFX.length);
        gameObject.SetActive(false);
    }

    AudioSource audioS;
    [SerializeField] AudioClip gatherSFX;
}
