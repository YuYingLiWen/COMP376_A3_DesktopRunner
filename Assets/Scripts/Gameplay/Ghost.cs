using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public sealed class Ghost : MonoBehaviour
{
    Health health;
    SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponentInChildren<SpriteRenderer>();

        if(!player)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        health = new Health(25);
    }

    private void Start()
    {
        StartCoroutine(ColorRoutine());
    }

    private void Update()
    {

        Vector3 dir = player.position - transform.position;

        transform.LookAt(player);

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);

        if (!health.IsAlive()) GhostPooler.Instance.Pool.Release(this);// Back to pooler
    }

    IEnumerator ColorRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            rend.flipX = !rend.flipX;
        }
    }

    private static Transform player = null;
    private static float speed = 0.75f;

}
