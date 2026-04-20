using DG.Tweening;
using UnityEngine;

public class LevelUiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject levelFailedPanel;
    [SerializeField]
    private GameObject levelCompletedPanel;

    [SerializeField]
    private GameObject[] hints;

    private static LevelUiManager instance;
    public static LevelUiManager Instance
    {
        get => instance;
    }

    protected void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    protected void Start()
    {
        if (hints[GameManager.Instance.CurrentLevelIndex - 1] != null)
        {
            hints[GameManager.Instance.CurrentLevelIndex - 1].transform.localScale = Vector3.zero;
            hints[GameManager.Instance.CurrentLevelIndex - 1].SetActive(true);
            hints[GameManager.Instance.CurrentLevelIndex - 1].transform.DOScale(1, 1f);
        }
    }

    public void ShowLevelFailedScreen()
    {
        levelFailedPanel.SetActive(true);
    }

    public void ShowLevelCompletedScreen()
    {
        levelCompletedPanel.SetActive(true);
    }
}