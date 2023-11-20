using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] AudioClip clip;
    AudioSource source;

    [SerializeField] GameObject toDisable;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(3);

            collision.rigidbody.AddExplosionForce(1000.0f, transform.position, 10.0f,500.0f);
            ps.Play();
            source.PlayOneShot(clip);
            toDisable.SetActive(false);
            StartCoroutine(Routine());
        }
    }

    IEnumerator Routine()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
