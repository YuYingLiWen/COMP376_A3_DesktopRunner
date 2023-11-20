using System.Collections;
using UnityEngine;

public sealed class Ghost : MonoBehaviour
{
    Health health;
    [SerializeField] SpriteRenderer rend;

    Rigidbody rigid;

    private void Awake()
    {
        if(!player)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        audioS = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        health = new Health(25);
    }

    private void Start()
    {
        StartCoroutine(FlipRoutine());
    }

    private void Update()
    {
        Vector3 dir = player.position - transform.position;

        transform.LookAt(player);

        rigid.MovePosition(rigid.position + dir.normalized * speed * Time.deltaTime);

        if (transform.position.y < -500.0f) // Falls off game world
        {
            health.InstantDeath();
            GhostPooler.Instance.Pool.Release(this);
        }
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        audioS.PlayOneShot(hitSFX[Random.Range(0,hitSFX.Length)]);

        if (!health.IsAlive())
        {
            GhostPooler.Instance.Pool.Release(this);// Back to pooler
            player.GetComponent<Player>().AddPoints(150);
        }
    }

    IEnumerator FlipRoutine()
    {
        while(true)
        {
            yield return waitForFlip;
            rend.flipX = !rend.flipX;
        }
    }

    WaitForSeconds waitForFlip = new WaitForSeconds(0.5f);
    private static Transform player = null;
    private static float speed = 1.5f;

    AudioSource audioS;
    [SerializeField] AudioClip[] hitSFX;
}
