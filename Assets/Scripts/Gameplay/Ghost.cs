using System.Collections;
using UnityEngine;

public sealed class Ghost : MonoBehaviour
{
    Health health;
    [SerializeField] SpriteRenderer rend;

    private void Awake()
    {
        if(!player)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody>();
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

        //rb.AddForce(dir.normalized * speed,ForceMode.Force);
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);

        if (!health.IsAlive()) GhostPooler.Instance.Pool.Release(this);// Back to pooler
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
    private static float speed = 1.0f;
    Rigidbody rb;
}
