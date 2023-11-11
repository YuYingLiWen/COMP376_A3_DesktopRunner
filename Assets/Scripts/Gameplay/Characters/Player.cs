using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int healthPoint = 10;

    [SerializeField] Health health;

    [SerializeField] Image hpBar;

    [SerializeField] Transform head;

     Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Instand Death
            Debug.Log("Instant Death");
        }
        else if (other.CompareTag("Spike"))
        {
            TakeDamage(1);
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
    }

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
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

        if (key_x != 0.0f) direction += transform.right * key_x;
        if (key_y != 0.0f) direction += transform.forward * key_y;

        if (Input.GetKeyDown(KeyCode.Space)) rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);

        rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.deltaTime);//= direction.normalized * moveSpeed;
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


    IEnumerator SpeedBoostRoutine()
    {
        yield return null;
    }

    IEnumerator InvincibilityRoutine()
    {
        yield break;
    }

    IEnumerator InfiniteAmmoRoutiine()
    {
        yield break;
    }

    IEnumerator FlyRoutine() 
    { 
        yield break;
    }

    IEnumerator HealthRegenRoutine()
    {
        yield break;
    }

    [SerializeField] float moveSpeed = 3.0f;
}
