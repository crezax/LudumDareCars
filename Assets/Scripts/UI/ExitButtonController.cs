using UnityEngine;

public class ExitButtonController : MonoBehaviour
{
    public void ExitToMenu()
    {
        GameManager.Instance.LoadMenu();
    }
}