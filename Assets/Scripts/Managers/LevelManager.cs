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
        OnGameOver += gameManager.HandleGameOver;
    }

    private void OnDisable()
    {
        if (debugMode) return;

        OnGameOver -= gameManager.HandleGameOver;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        SceneDirector.GetInstance().Load(SceneDirector.SceneNames.CREDITS_SCENE, true);
    }

    public void SpawnNextTunnel(Vector3 anchor)
    {
        var t = Instantiate(tunnel);
        t.transform.position = anchor;
    }

    [SerializeField] GameObject tunnel;

    private static LevelManager instance;
    public static LevelManager Instance => instance;
}
