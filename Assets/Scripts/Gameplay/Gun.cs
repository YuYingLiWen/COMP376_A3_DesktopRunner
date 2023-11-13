
using UnityEngine;

public sealed class Gun : MonoBehaviour
{
    Animator control;

    [SerializeField] Reload reload;

    [SerializeField] AudioSource fireSFX;
    [SerializeField] ParticleSystem fireVFX;

    [SerializeField] Transform rifle;

    bool isHolding = false;

    private void Awake()
    {
        control = GetComponent<Animator>();
        fireSFX = GetComponent<AudioSource>();

        cam = Camera.main.transform;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            isHolding = true;
            if(!canHold) Fire();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            isHolding= false;
        }

        if(isHolding && canHold)
        {
            if (elapsedTime >= delayPerShot)
            {
                Fire();
                elapsedTime = 0.0f;
            }

            elapsedTime += Time.deltaTime;
        }
    }

    void Fire()
    {
        if (!reload.CanFire) return;

        control.SetTrigger("Fire");
        fireSFX.Play();
        fireVFX.Play();
        reload.OnFire();// UI image bullet;

        Vector3 aimCircle = Random.insideUnitCircle * angle;

        var trail = ProjectileTrailPooler.Instance.Pool.Get();

        Ray ray = new(cam.position, cam.forward + aimCircle);

        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 5.0f);
        if (Physics.Raycast(ray,out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
        {
            Debug.Log(hit.collider.name);

            trail.SetPositions(rifle.position, hit.point);

            if (hit.collider.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Ghost>().TakeDamage(1);
            }
        }
        else
        {
            trail.SetPositions(rifle.position, cam.forward * distance + aimCircle);
        }
    }

    public void Upgrade()
    {
        canHold = true;
    }

    private bool canHold = false;

    // Cache
    Transform cam;

    [SerializeField] int attackPoints = 5; // Damage per shot fired
    [SerializeField] float angle = 0.04f;
    [SerializeField] float distance = 500.0f;

    float elapsedTime = 0.0f;
    [SerializeField] float delayPerShot = 0.1f;
}
