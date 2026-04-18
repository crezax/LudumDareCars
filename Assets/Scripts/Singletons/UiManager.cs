using DG.Tweening;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }
    public GameObject levelCompletedScreen;
    public LoadingScreenManager loadingScreen;

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

    public void ShowLevelCompletedScreen()
    {
        Debug.Log("Showing level completed screen!");
    }

    public void ShowLevelFailedScreen()
    {
        Debug.Log("Showing level failed screen!");
    }

    public void ShowLoadingScreenForLevel(int levelIndex)
    {
        loadingScreen.Show(levelIndex);
    }

    public void HideLoadingScreen()
    {
        loadingScreen.Hide();
    }
}