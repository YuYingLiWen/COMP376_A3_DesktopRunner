
using UnityEngine;
using UnityEngine.UI;

public sealed class Reload : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Image reload;

    const int upgradedCount = 45;
    const int normalCount = 15;

    int currentBullet = 0;

    [SerializeField] bool canFire = true;
    public float ReloadTime = 3.0f;
    private float elapsedReloadTime = 0.0f;

    public bool CanFire => canFire;
    public void OnFire()
    {
        if (!canFire) return;

        currentBullet -= 1;
        bullets[currentBullet].SetActive(false);

        if(currentBullet == 0) canFire = false;
    }

    private void OnEnable()
    {
        Activate(normalCount);
    }

    private void Update()
    {
        Reloading();
    }

    private void Reloading()
    {
        if (canFire) return; // If can fire then don't need reload.

        if (elapsedReloadTime >= ReloadTime)
        {
            OnReloaded();
            reload.fillAmount = 0.0f;
            return;
        }

        reload.fillAmount = elapsedReloadTime / ReloadTime;
        elapsedReloadTime += Time.deltaTime;
    }

    public void Upgrade()
    {
        hasUpgrade = true;
    }

    void OnReloaded()
    {
        canFire = true;
        elapsedReloadTime = 0.0f;

        if(hasUpgrade)
        {
            reload.fillAmount = 1.0f;
            Activate(upgradedCount);
        }
        else
        {
            reload.fillAmount = 1.0f;
            Activate(normalCount);
        }
    }

    void Activate(int count)
    {
        for(int i = 0; i < count; i++)
        {
            bullets[i].SetActive(true);
        }

        currentBullet = count;
    }

    private bool hasUpgrade = false;

}
