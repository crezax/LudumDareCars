using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    protected void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LevelCompleted()
    {
        Debug.Log("Level completed!");
        TurnManager.Instance.enabled = false;
        UiManager.Instance.ShowLevelCompletedScreen();
    }

    public void LevelFailed()
    {
        Debug.Log("Level failed!");
        TurnManager.Instance.enabled = false;
        UiManager.Instance.ShowLevelFailedScreen();
    }

    public void LoadLevel(int levelIndex)
    {
        UiManager.Instance.ShowLoadingScreenForLevel(levelIndex);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Level" + levelIndex).completed += (AsyncOperation obj) =>
        {
            UiManager.Instance.HideLoadingScreen();
            TurnManager.Instance.enabled = true;
        };
    }
}