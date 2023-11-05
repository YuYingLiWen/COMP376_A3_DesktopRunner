using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int healthPoint = 10;
    [SerializeField] private int attackPoints = 5; // Damage per shot fired

    [SerializeField] private Health health;

    [SerializeField] private Transform rifle;
    [SerializeField] private AimPoint aimPoint;

    [SerializeField] private float firingAngle = 0.5f;
    [SerializeField] private float aimSpeed = 3.0f;
    private CircleCollider2D coll;

    [SerializeField] private Reload reload;

    [SerializeField] private Image hpBar;

    private AudioSource fireSFX;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();

        fireSFX = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
    }

    private void Start()
    {
    }

    private void OnDisable()
    {
    }

    void Update()
    {
    }

    [ContextMenu("Fire()")]
    void Fire()
    {
        if (!reload.CanFire) return;

        fireSFX.Play();
        reload.OnFire();// UI image bullet;

        Vector2 aimCircle = Random.insideUnitCircle * firingAngle;

        // Check if distance is too close to player 
        float aimDistance = (aimCircle + aimPoint.WorldPoint - (Vector2)transform.position).magnitude;
        if (aimDistance <= 1.0f) return; // Too close to self

        RaycastHit2D pointer = aimPoint.Raycast(transform.position, aimCircle);  //TODO: Check Layer Mask

        // Check if hit anything
        Collider2D collider = pointer.collider;

        var trail = ProjectileTrailPooler.Instance.Pool.Get();

        if (!collider)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= 0.8f) // 80% chance to shoot to shoot dirt
            {
                trail.SetPositions(rifle.position, aimPoint.WorldPoint + aimCircle);
            }
            else // WildShot 20% chance
            {
                trail.SetWildShot(rifle.position, transform.up, aimCircle);
            }
        }
        else
        {
            if (collider.CompareTag("Enemy"))
            {


                trail.SetPositions(rifle.position, pointer.point);
            }
        }
    }

    [ContextMenu("Take DAmage")]
    void debugTAke()
    {
        health.TakeDamage(1);
        hpBar.fillAmount = health.GetHealthPercent();

    }

    public void TakeDamage(int damage, Vector3 at, Vector3 up)
    {
        health.TakeDamage(damage);

        hpBar.fillAmount = health.GetHealthPercent();

        if (!health.IsAlive()) FindAnyObjectByType<LevelManager>().GameOver();
    }

    public void SpawnBlood(Vector3 at, Vector3 up)
    {
        GameObject blood = BloodPooler.Instance.Pool.Get();
        blood.transform.up = at - up;
        blood.transform.position = at;
    }

    public Transform GetTransform()
    {
        return transform;
    }


    private IEnumerator SpeedBoostRoutine()
    {
        yield return null;
    }

    private IEnumerator InvincibilityRoutine()
    {
        yield break;
    }

    private IEnumerator InfiniteAmmoRoutiine()
    {
        yield break;
    }

    private IEnumerator FlyRoutine() 
    { 
        yield break;
    }

    private IEnumerator MoreLightRoutine()
    {
        yield break;
    }
}
