
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

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
        else if (other.CompareTag("Fly"))
        {
            fly.SetActive(true);   
        }
        else if (other.CompareTag("Health"))
        {
            health.AddHealth(1);
        }
        else if (other.CompareTag("Shield"))
        {
            invincibility.SetActive(true);
        }
        else if (other.CompareTag("Bullets"))
        {
            infiniteAmmo.SetActive(true);
        }
        else if (other.CompareTag("Speed"))
        {
            speedBoost.SetActive(true);
        }
        else if (other.CompareTag("AmmoBox"))
        {
            UpgradeAmmo();
        }
        else if (other.CompareTag("Pill"))
        {
            UpgradeSpeed();
        }
        else if (other.CompareTag("Gun"))
        {
            UpgradeGun();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Spike"))
        {
            TakeDamage(1);
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        health = new Health(healthPoint);
    }

    void OnEnable()
    {
        infiniteAmmo.OnComplete += HandleInfiniteAmmo;
        fly.OnComplete += HandleFly;
        speedBoost.OnComplete += HandleSpeedBoost;
        invincibility.OnComplete += HandleInvincibility;
    }

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouse_x * Time.deltaTime * 100.0f);

        rotation -= mouse_y * Time.deltaTime * 100.0f;
        rotation = Mathf.Clamp(rotation, -90.0f, 90.0f);
        head.localEulerAngles = Vector3.right * rotation  ;

        if (Input.GetKeyDown(KeyCode.Space)) rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);

        float key_x = Input.GetAxis("Horizontal");
        float key_y = Input.GetAxis("Vertical");

        Vector3 direction = Vector3.zero;

        if (key_x != 0.0f) direction += transform.right * key_x;
        if (key_y != 0.0f) direction += transform.forward * key_y;

        rb.MovePosition(rb.position + direction.normalized * (moveSpeed + speedUpgrade) * Time.deltaTime);

        //rb.AddForce(direction.normalized * (moveSpeed + speedUpgrade), ForceMode.Acceleration);
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


    void HandleSpeedBoost()
    {
        hasSpeedBoost = false;
        speedBoost.SetActive(hasSpeedBoost);
    }

    void HandleInvincibility()
    {
        hasInvincibility = false;
        invincibility.SetActive(hasInvincibility);
    }

    void HandleInfiniteAmmo()
    {
        hasInfiniteAmmo = false;
        infiniteAmmo.SetActive(hasInfiniteAmmo);
    }

    void HandleFly() 
    { 
        canFly = false;
        fly.SetActive(canFly);
    }

    void UpgradeAmmo()
    {
        ammoUpgrade += 1;
        if (ammoUpgrade >= 3) reload.Upgrade();
        UpdateUpgradeText();
    }

    void UpgradeSpeed()
    {
        if (speedUpgrade >= 3) return;
        speedUpgrade += 1;
        UpdateUpgradeText();
    }

    void UpgradeGun()
    {
        gunUpgrade += 1;
        if (gunUpgrade >= 3) gun.Upgrade();
        UpdateUpgradeText();
    }

    void UpdateUpgradeText()
    {
        upgradeText.text = $"UPGRADES\r\n-----------------\r\n\r\nAmmo: {ammoUpgrade}/3\r\n\r\nSpeed: {speedUpgrade}/3\r\n\r\nGun: {gunUpgrade}/3";
    }

    [SerializeField] int healthPoint = 10;
    Health health;

    [SerializeField] Reload reload;
    [SerializeField] Gun gun;

    [SerializeField] float moveSpeed = 3.0f;
    float rotation = 0.0f;

    [SerializeField] PowerUpUI infiniteAmmo;
    [SerializeField] PowerUpUI fly;
    [SerializeField] PowerUpUI speedBoost;
    [SerializeField] PowerUpUI invincibility;

    bool hasInfiniteAmmo = false;
    bool canFly = false;
    bool hasSpeedBoost = false;
    bool hasInvincibility = false;

    int gunUpgrade = 0;
    int speedUpgrade = 0;
    int ammoUpgrade = 0;

    [SerializeField] TMP_Text upgradeText;
}
