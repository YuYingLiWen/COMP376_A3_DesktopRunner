using TMPro;
using UnityEngine;

/// <summary>
/// Goal: Checks win & lose conditions.
/// </summary>

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;

    public bool debugMode = false;
    float timeElapsed = 0.0f;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        if (!gameManager)
        {
            Debug.LogError("Missing Game Manager", gameObject);
            debugMode = true;
        }
    }

    private void Start()
    {
        gameManager.Unpause();
    }

    private void OnEnable()
    {
        if (debugMode) return;

        GameManager.Instance.OnGamePause += HandlePause;
        GameManager.Instance.OnGameUnpause += HandleUnpause;

    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > GameManager.Instance.GetMaxLevelTime())
        {
            levelTimeEnded = true;
        }
    }

    private void OnDisable()
    {
        if (debugMode) return;

        GameManager.Instance.OnGamePause -= HandlePause;
        GameManager.Instance.OnGameUnpause -= HandleUnpause;

    }

    void HandlePause()
    {
        pauseUI.SetActive(true);
    }

    void HandleUnpause()
    {
        pauseUI.SetActive(false);
    }

    public bool LevelTimePassed()
    {
        return levelTimeEnded;
    }

    public void GameOver()
    {
        GameManager.Instance.PlayerIsDead();
        deathUI.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;

        deathUIScoreText.text = $"HIGHSCORE: {GameManager.Instance.GetHighScore}\r\nCURRENT: {GameManager.Instance.GetCurrentScore}";
    }

    public void SpawnNextTunnel(Vector3 anchor)
    {
        var t = Instantiate(tunnels[Random.Range(0,tunnels.Length)]);
        t.transform.position = anchor;
    }

    public void LoadMainMenu()
    {
        SceneDirector.GetInstance().Load(SceneDirector.SceneNames.MAIN_MENU_SCENE, true);
    }

    public void Retry()
    {
        SceneDirector.GetInstance().Load(SceneDirector.SceneNames.CHAPTER1_SCENE, true);
    }


    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject deathUI;

    [SerializeField] TMP_Text deathUIScoreText;

    [SerializeField] GameObject[] tunnels;


    bool levelTimeEnded = false;
}
