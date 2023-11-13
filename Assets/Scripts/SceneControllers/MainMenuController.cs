
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private SceneDirector sceneDirector;

    private void Awake()
    {
        sceneDirector = SceneDirector.GetInstance();
        if (!sceneDirector) Debug.LogError("Missing SceneDirector.", gameObject);
    }

    public void LoadLevel1()
    {
        Debug.Log("Clieck load ");
        sceneDirector.Load(SceneDirector.SceneNames.CHAPTER1_SCENE, true);
    }

    public void PlayHard()
    {
        GameManager.Instance.SetDifficulty(GameManager.Difficulty.Hard);

        LoadLevel1();
    }

    public void PlayMedium()
    {
        GameManager.Instance.SetDifficulty(GameManager.Difficulty.Medium);

        LoadLevel1();
    }

    public void PlayEasy()
    {
        GameManager.Instance.SetDifficulty(GameManager.Difficulty.Easy);

        LoadLevel1();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
