
using UnityEngine;
public sealed class Floor : MonoBehaviour
{
    [SerializeField] GameObject[] staticObstacles;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] GameObject[] permaUpgrade;
    [SerializeField] GameObject bear;


    static DifficultySO so = null;

    private void Awake()
    {
        if(Random.Range(0.0f,1.0f) < 0.1f)
        {
            gameObject.SetActive(false);
            return;
        }

        if (so == null) so = GameManager.Instance.GetDifficultyData();



        if (Random.Range(0.0f, 100.0f) < so.ObsctacleChance)
        {
            var obstacle = Instantiate(staticObstacles[Random.Range(0, staticObstacles.Length)]);
            obstacle.transform.parent = transform;
            obstacle.transform.position = transform.position ;
        }
        else if (Random.Range(0.0f, 100.0f) < so.BearChance)
        {
            var obstacle = Instantiate(bear);
            obstacle.transform.parent = transform;
            obstacle.transform.position = transform.position;
        }

        if (Random.Range(0.0f, 100.0f) < so.PowerUpChance)
        {
            var power = Instantiate(powerUps[Random.Range(0, powerUps.Length)]);
            power.transform.parent = transform;
            power.transform.position = transform.position + Vector3.up * 0.75f;
        }
        else if (Random.Range(0.0f, 100.0f) < so.UpgradeChance)
        {
            var upgrade = Instantiate(permaUpgrade[Random.Range(0, permaUpgrade.Length)]);
            upgrade.transform.parent = transform;
            upgrade.transform.position = transform.position + Vector3.up ;
        }
    }
}
