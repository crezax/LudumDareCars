using DG.Tweening;
using TMPro;
using UnityEngine;

public class InitialScreenController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    protected void Start()
    {
        gameObject.SetActive(!MainMenuUiController.Instance.InitialScreenClicked);
        text.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnClick()
    {
        MainMenuUiController.Instance.InitialScreenClicked = true;
        gameObject.SetActive(false);
    }
}