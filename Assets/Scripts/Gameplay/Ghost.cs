using System.Collections;
using UnityEngine;

public sealed class Ghost : MonoBehaviour
{
    Health health;
    Renderer rend;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();

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
            yield return new WaitForSeconds(0.1f);
            rend.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        }
    }

    private static Transform player = null;
    private static float speed = 0.75f;

}
