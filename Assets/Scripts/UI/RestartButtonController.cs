using UnityEngine;

public class RestartButtonController : MonoBehaviour
{
    public void RestartLevel()
    {
        GameManager.Instance.RestartLevel();
    }
}