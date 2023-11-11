
using UnityEngine;

public sealed class BearSpawner : MonoBehaviour
{

    [SerializeField] float delayBetweenSpawn = 5.0f;
    float elapsedTime = 0.0f;

    void Update()
    {
        if(elapsedTime >= delayBetweenSpawn)
        {
            Spawn();
            elapsedTime = 0.0f;
        }
        elapsedTime += Time.deltaTime;
    }

    void Spawn()
    {
        var ghost =  GhostPooler.Instance.Pool.Get();
        ghost.transform.position = transform.position;
    }
}
