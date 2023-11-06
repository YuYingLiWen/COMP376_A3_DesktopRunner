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

    [SerializeField] private Reload reload;

    [SerializeField] private Image hpBar;

    [SerializeField] private Transform head;

    private Rigidbody rb;

    private AudioSource fireSFX;

    private void Awake()
    {
        fireSFX = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
    }

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
    }

    void Update()
    {
        float key_x = Input.GetAxis("Horizontal");
        float key_y = Input.GetAxis("Vertical");

        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouse_x * Time.deltaTime * 100.0f);
        head.Rotate(Vector3.right * -mouse_y * Time.deltaTime * 100.0f);

        Vector3 direction = Vector3.zero;

        if (key_x != 0.0f)
            //rb.MovePosition(rb.position + transform.right *moveSpeed* key_x * Time.deltaTime);
            direction += transform.right * key_x;
        if(key_y != 0.0f)
            
            //rb.MovePosition(rb.position + transform.forward *moveSpeed *  key_y * Time.deltaTime);

         direction += transform.forward * key_y;

        rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.deltaTime);//= direction.normalized * moveSpeed;
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

    public void TakeDamage(int damage)
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

    [SerializeField] private float moveSpeed = 3.0f;
}
