using System;
using UnityEngine;
using UnityEngine.UI;


public sealed class PowerUpUI : MonoBehaviour
{
    Image reload;

    [SerializeField] float reloadTime = 3.0f;
    private float elapsedReloadTime = 0.0f;

    private void Awake()
    {
        reload = GetComponent<Image>();
    }

    private void OnEnable()
    {
        elapsedReloadTime = 0.0f;
        reload.fillAmount = 1.0f;
    }

    private void Update()
    {
        Reloading();
    }

    private void Reloading()
    {
        if (elapsedReloadTime >= reloadTime)
        {
            OnComplete?.Invoke();
            gameObject.SetActive(false);
            return;
        }

        reload.fillAmount = 1.0f - (elapsedReloadTime / reloadTime);
        elapsedReloadTime += Time.deltaTime;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public Action OnComplete;
}
