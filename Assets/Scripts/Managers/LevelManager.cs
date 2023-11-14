using System;
using UnityEngine;

/// <summary>
/// Goal: Checks win & lose conditions.
/// </summary>

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;

    public Action OnGameOver;
    public Action OnGameWon;

    public bool debugMode = false;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        if (!gameManager)
        {
            Debug.LogError("Missing Game Manager", gameObject);
            debugMode = true;
        }

        if (!instance) instance = this;
        else Debug.LogWarning("Multiple " + this.GetType().Name, this);
    }

    private void OnEnable()
    {
        if (debugMode) return;

        GameManager.Instance.OnGamePause += HandlePause;
        GameManager.Instance.OnGameUnpause += HandleUnpause;
    }


    void HandlePause()
    {
        pauseUI.SetActive(true);
    }

    void HandleUnpause()
    {
        pauseUI.SetActive(false);
    }


    public void GameOver()
    {
        OnGameOver?.Invoke();

        deathUI.SetActive(true);
    }

    public void SpawnNextTunnel(Vector3 anchor)
    {
        var t = Instantiate(tunnel);
        t.transform.position = anchor;
    }

    public void LoadMainMenu()
    {
        SceneDirector.GetInstance().Load(SceneDirector.SceneNames.MAIN_MENU_SCENE, true);
    }


    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject deathUI;


    [SerializeField] GameObject tunnel;

    private static LevelManager instance;
    public static LevelManager Instance => instance;
}
