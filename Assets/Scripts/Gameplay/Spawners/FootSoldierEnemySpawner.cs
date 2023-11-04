
using UnityEngine;

public class FootSoldierEnemySpawner : MonoBehaviour
{
    private Camera cam;

    private Vector3 outOfBoundUp;

    [SerializeField] float outOfBoundDistance = 2.0f;

    [SerializeField] float waveDelay = 10.0f;

    float elapsedTime = 0.0f;

    [SerializeField] private int enemyCountPerWave = 3;

    public void IncreaseEnemyCountPerWave()
    {
        enemyCountPerWave += 1;
    }

    private void Awake()
    {
        cam = Camera.main;

        outOfBoundUp = cam.transform.position + Vector3.up * (cam.orthographicSize + outOfBoundDistance);
    }

    void Update()
    {
        if(elapsedTime >= waveDelay)
        {
            SendWave();
            elapsedTime = 0.0f;
        }
        elapsedTime += Time.deltaTime;
    }

    void SendWave()
    {
        for (int i = 0; i < enemyCountPerWave; i++)
        {

        }

    }
}
