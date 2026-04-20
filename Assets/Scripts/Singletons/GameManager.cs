using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int currentLevelIndex = 1;
    public int CurrentLevelIndex
    {
        get => currentLevelIndex;
    }

    protected void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LevelCompleted()
    {
        Debug.Log("Level completed!");
        TurnManager.Instance.enabled = false;
        LevelUiManager.Instance.ShowLevelCompletedScreen();
    }

    public void LevelFailed()
    {
        Debug.Log("Level failed!");
        TurnManager.Instance.enabled = false;
        LevelUiManager.Instance.ShowLevelFailedScreen();
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }

    public void LoadNextLevel()
    {
        if (IsLastLevel())
        {
            Debug.Log("No more levels! Returning to main menu.");
            LoadMenu();
            return;
        }
        LoadLevel(currentLevelIndex + 1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadSceneAsync("SampleScene").completed += (AsyncOperation op) =>
        {
            currentLevelIndex = levelIndex;
            LevelManager.Instance.EnableLevel(levelIndex);
        };
    }

    public bool IsLastLevel()
    {
        return currentLevelIndex >= LevelManager.Instance.LevelsCount;
    }
}