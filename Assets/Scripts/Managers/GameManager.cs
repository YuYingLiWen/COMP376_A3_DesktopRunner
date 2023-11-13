
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
    private enum GameState { PLAY, PAUSED, MAIN_MENU, CHAPTER1, CREDITS };
    private GameState currentGameState = GameState.MAIN_MENU;

    public Action OnGamePause;

    private void Awake()
    {
        if(!instance) instance = this;
        else Destroy(gameObject);

        sceneDirector = SceneDirector.GetInstance();
        if (!sceneDirector) Debug.LogError("Missing Scene Director.", gameObject);

        audioManager = AudioManager.Instance;
        if (!audioManager) Debug.LogError("Missing Audio Manager", gameObject);
    }

    private void OnEnable()
    {
        sceneDirector.OnSceneActivated += HandleLevelSceneActivation;
        sceneDirector.OnSceneActivated += HandleMainMenuSceneActivation;
    }

    public void HandleLevelSceneActivation(string sceneName)
    {
        if (!sceneName.Contains("Level")) return;

        currentGameState = GameState.PLAY;
    }

    public void HandleCreditsSceneActivation()
    {
        currentGameState = GameState.CREDITS;

    }

    public void HandleMainMenuSceneActivation(string sceneName)
    {
        if (!sceneName.Equals(SceneDirector.SceneNames.MAIN_MENU_SCENE)) return;

        currentGameState = GameState.MAIN_MENU;

        audioManager.Play(sceneName);
    }

    public void HandleGameOver()
    {
       
    }

    [SerializeField] DifficultySO easy;
    [SerializeField] DifficultySO medium;
    [SerializeField] DifficultySO hard;
    [SerializeField] DifficultySO debug;

    public DifficultySO GetDifficultyData()
    {
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

    public void SetDifficulty(Difficulty difficulty) => this.difficulty = difficulty;
    public enum Difficulty { Easy, Hard, Medium};
    Difficulty difficulty = Difficulty.Easy;
}

