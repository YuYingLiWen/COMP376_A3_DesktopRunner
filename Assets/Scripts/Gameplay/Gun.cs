
using UnityEngine;

public sealed class Gun : MonoBehaviour
{
    Animator control;

    [SerializeField] Reload reload;

    [SerializeField] ParticleSystem fireVFX;

    [SerializeField] Transform rifle;

    bool isHolding = false;

    private void Awake()
    {
        control = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();

        cam = Camera.main.transform;
    }

    void Update()
    {
        if (GameManager.Instance.GetGameState == GameManager.GameState.PAUSED) return;


        if (Input.GetKeyDown(KeyCode.Mouse0))
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
        audioS.Play();
        fireVFX.Play();
        audioS.PlayOneShot(fireSFX);
        reload.OnFire();// UI image bullet;

        //Vector3 aimCircle = Random.insideUnitCircle * angle;

        var trail = ProjectileTrailPooler.Instance.Pool.Get();

        //Ray ray = new(cam.position, cam.forward + aimCircle);
        Ray ray = new(cam.position, cam.forward );


        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 5.0f);
        if (Physics.Raycast(ray,out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Enemies","Level","Spikes","Default"), QueryTriggerInteraction.Ignore))
        {

            trail.SetPositions(rifle.position, hit.point);

            if (hit.collider.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Ghost>().TakeDamage(1);
                //var blood = BloodPooler.Instance.Pool.Get();
                //blood.transform.position = hit.point;
            }
            else if (hit.collider.CompareTag("EnemyHead"))
            {
                hit.transform.GetComponent<Ghost>().TakeDamage(5);
                var blood = BloodPooler.Instance.Pool.Get();
                blood.transform.position = hit.point;
            }
        }
        else
        {
            //trail.SetPositions(rifle.position, cam.forward * distance + aimCircle);
            trail.SetPositions(rifle.position, cam.forward * distance);
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

    AudioSource audioS;
    [SerializeField] AudioClip fireSFX;
}
