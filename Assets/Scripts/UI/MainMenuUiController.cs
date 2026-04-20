using UnityEngine;

public class MainMenuUiController : MonoBehaviour
{
    private static MainMenuUiController instance;
    public static MainMenuUiController Instance
    {
        get => instance;
    }

    private bool initialScreenClicked = false;
    public bool InitialScreenClicked
    {
        get => initialScreenClicked;
        set => initialScreenClicked = value;
    }

    protected void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}