
using UnityEngine;
public sealed class Floor : MonoBehaviour
{
    [SerializeField] GameObject[] staticObstacles;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] GameObject[] permaUpgrade;
    [SerializeField] GameObject bear;


    private void Awake()
    {
        if(Random.Range(0.0f,1.0f) < 0.1f)
        {
            gameObject.SetActive(false);
            return;
        }

        if (Random.Range(0.0f, 1.0f) < 0.07f)
        {
            var obstacle = Instantiate(staticObstacles[Random.Range(0, staticObstacles.Length)]);
            obstacle.transform.parent = transform;
            obstacle.transform.position = transform.position ;
        }
        
        else if (Random.Range(0.0f, 1.0f) < 1.05f)
        {
            var power = Instantiate(powerUps[Random.Range(0, powerUps.Length)]);
            power.transform.parent = transform;
            power.transform.position = transform.position + Vector3.up * 0.75f;
        }
        
        else if (Random.Range(0.0f, 1.0f) < 1.01f)
        {
            var upgrade = Instantiate(permaUpgrade[Random.Range(0, permaUpgrade.Length)]);
            upgrade.transform.parent = transform;
            upgrade.transform.position = transform.position + Vector3.up ;
        }
         
        else if(Random.Range(0.0f, 1.0f) < 1.01f)
        {
            var obstacle = Instantiate(bear);
            obstacle.transform.parent = transform;
            obstacle.transform.position = transform.position;
        }
    }
}
