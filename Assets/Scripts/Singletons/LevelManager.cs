using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private GameObject[] levels;

    public int LevelsCount => levels.Length;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void EnableLevel(int levelIndex)
    {
        levels[levelIndex - 1].SetActive(true);
    }
}