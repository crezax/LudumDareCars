using Unity.VisualScripting;
using UnityEngine;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField]
    private int levelIndex;

    public void LoadLevel()
    {
        GameManager.Instance.LoadLevel(levelIndex);
    }
}