using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles anything that has to do with loading, unloading scene and scene activation.
/// </summary>

public class SceneDirector : MonoBehaviour
{
    private static SceneDirector instance = null;

    private GameManager gameManager;

    private AsyncOperation asyncOps = null;
    private Coroutine loadRoutine = null;

    public Action<GameManager.GameState> OnSceneActivated;

    public class SceneNames
    {
        public const string MAIN_MENU_SCENE = "MainMenuScene";
        public const string CHAPTER1_SCENE = "Chapter1";
    }

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        if (!gameManager) Debug.LogError("Missing Game Manager", gameObject);
    }

    public static SceneDirector GetInstance() => instance;

    public void Load(string sceneName, bool activate)
    {
        if (loadRoutine == null) loadRoutine = StartCoroutine(LoadSceneCoroutine(sceneName, activate));
    }

    public void ActivateLoadedScene(string sceneName)
    {
        if (asyncOps == null) return;

        if(asyncOps.isDone)
        {
            asyncOps.allowSceneActivation = true;
            asyncOps.allowSceneActivation = false;
            asyncOps = null;
            loadRoutine = null;

            if(sceneName == SceneNames.CHAPTER1_SCENE) OnSceneActivated?.Invoke(GameManager.GameState.LEVEL);
            else if (sceneName == SceneNames.MAIN_MENU_SCENE) OnSceneActivated?.Invoke(GameManager.GameState.MAIN_MENU);
        }
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, bool activate)
    {
        asyncOps = SceneManager.LoadSceneAsync(sceneName);

        while(!asyncOps.isDone)
        {
            yield return null;
        }

        if(activate) ActivateLoadedScene(sceneName);
    }
}
