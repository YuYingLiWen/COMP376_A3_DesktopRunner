
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
        if (other.CompareTag("Fly"))
        {
            canFly = true;
            if (fly.IsActive) fly.ResetTimer();
            else fly.SetActive(true);   
        }
        else if (other.CompareTag("Health"))
        {
            health.AddHealth(1);
            UpdateHealthUI();
        }
        else if (other.CompareTag("Shield"))
        {
            hasInvincibility = true;
            if (invincibility.IsActive) invincibility.ResetTimer();
            else invincibility.SetActive(hasInvincibility);
        }
        else if (other.CompareTag("Bullets"))
        {
            hasInfiniteAmmo = true;
            reload.SetHasInfAmmo(hasInfiniteAmmo);

            if (infiniteAmmo.IsActive) infiniteAmmo.ResetTimer();
            else infiniteAmmo.SetActive(hasInfiniteAmmo);
        }
        else if (other.CompareTag("Speed"))
        {
            hasSpeedBoost = true;
            if (speedBoost.IsActive) speedBoost.ResetTimer();
            else speedBoost.SetActive(hasSpeedBoost);
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
            if(!hasInvincibility)
            {
                TakeDamage(1);
            }
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            //Instand Death
            Debug.Log("Instant Death");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            canJump = false;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        health = new Health(healthPoint);
        UpdateHealthUI();
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
        head.localEulerAngles = Vector3.right * rotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(canFly) rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
            else
            {
                if(canJump)
                {
                    canJump = false;
                    rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
                }
            }
        }
            

        float key_x = Input.GetAxis("Horizontal");
        float key_y = Input.GetAxis("Vertical");

        Vector3 direction = Vector3.zero;

        if (key_x != 0.0f) direction += transform.right * key_x;
        if (key_y != 0.0f) direction += transform.forward * key_y;

        if(!speedBoost) rb.MovePosition(rb.position + direction.normalized * (moveSpeed + speedUpgrade) * Time.deltaTime);
        else rb.MovePosition(rb.position + direction.normalized * (moveSpeed + speedUpgrade + 1.0f) * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        UpdateHealthUI();

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
        reload.SetHasInfAmmo(hasInfiniteAmmo);
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

    void UpdateHealthUI()
    {
        hpText.text = $"HP {health.Points} / {health.MaxPoints}";
        hpBar.fillAmount = health.GetHealthPercent();
    }

    bool canJump = true;

    [SerializeField] int healthPoint = 10;
    [SerializeField] TMP_Text hpText;
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
