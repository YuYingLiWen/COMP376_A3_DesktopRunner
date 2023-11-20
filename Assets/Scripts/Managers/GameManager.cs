
using System;
using UnityEngine;

/// <summary>
/// Goal: Manages the overall game system.
/// </summary>

public class GameManager : MonoBehaviour
{
    // Note: Script MUST be attached to DDOL
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    private SceneDirector sceneDirector = null;
    private AudioManager audioManager = null;

    // Game Pause
    public enum GameState { PAUSED, MAIN_MENU, LEVEL };
    private GameState currentGameState = GameState.MAIN_MENU;

    public GameState GetGameState => currentGameState;

    public Action OnGamePause;
    public Action OnGameUnpause;

    private void Awake()
    {
        if(!instance) instance = this;
        else Destroy(gameObject);

        sceneDirector = SceneDirector.GetInstance();
        if (!sceneDirector) Debug.LogError("Missing Scene Director.", gameObject);

        audioManager = AudioManager.Instance;
        if (!audioManager) Debug.LogError("Missing Audio Manager", gameObject);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void OnEnable()
    {
        sceneDirector.OnSceneActivated += HandleLevelSceneActivation;
        sceneDirector.OnSceneActivated += HandleMainMenuSceneActivation;
    }

    private void Update()
    {
        if (currentGameState == GameState.MAIN_MENU) return;

        if (playerIsDead) return;
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    private void TogglePause()
    {
        if (isPaused) // if paused
        {
            Unpause();
        }
        else // Not paused
        {
            Pause();
        }
        isPaused = !isPaused;
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
        OnGamePause?.Invoke();
        currentGameState = GameState.PAUSED;
    }

    public void Unpause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        OnGameUnpause?.Invoke();
        currentGameState = GameState.LEVEL;
    }

    public void HandleLevelSceneActivation(GameState state)
    {
        if (state != GameState.LEVEL) return;

        currentGameState = GameState.LEVEL;
        playerIsDead = false;
    }

    public void HandleMainMenuSceneActivation(GameState state)
    {
        if (state != GameState.MAIN_MENU) return;

        currentGameState = GameState.MAIN_MENU;

        audioManager.Play(state);
        Time.timeScale = 1.0f;
    }

    [SerializeField] DifficultySO easy;
    [SerializeField] DifficultySO medium;
    [SerializeField] DifficultySO hard;
    [SerializeField] DifficultySO debug;

    public DifficultySO GetDifficultyData()
    {
        Debug.Log("Difficulty: " + difficulty.ToString());
        switch (this.difficulty)
        {
            case Difficulty.Easy:
                return easy;
            case Difficulty.Hard:
                return hard;
            case Difficulty.Medium:
                return medium;
            default:
                return debug;
        }
    }

    public float GetMaxLevelTime()
    {
        return GetDifficultyData().LevelTime;
    }

    public void PlayerIsDead()
    {
        playerIsDead = true;
        currentGameState = GameState.PAUSED;
    }

    bool isPaused = false;
    bool playerIsDead = false;

    public void SetDifficulty(Difficulty difficulty)
    {
        this.difficulty = difficulty;
    }

    public enum Difficulty { Easy, Hard, Medium};
    Difficulty difficulty = Difficulty.Easy;

    int highScore = 0;
    int currentScore = 0;

    public int GetHighScore => highScore;
    public int GetCurrentScore => currentScore;

    public void SetHighScore(int score)
    {
        highScore = Mathf.Max(highScore, score);
    }

    public void SetCurrentScore(int score)
    {
        currentScore = score;
    }
}

