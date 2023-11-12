
using UnityEngine;
using UnityEngine.UI;

public sealed class Reload : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Image reload;

    const int count = 45;
    int currentBullet = 45;

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

    void OnReloaded()
    {
        canFire = true;
        elapsedReloadTime = 0.0f;
        currentBullet = count;
        reload.fillAmount = 1.0f;
        foreach(GameObject obj in bullets) obj.SetActive(true);
    }
}
