using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private Image backgroundImage;

    public void Show(int levelIndex)
    {
        loadingText.text = $"Level {levelIndex}";
        foreach (var t in GetComponentsInChildren<Graphic>())
        {
            // Make all graphics visible and fully opaque
            t.gameObject.SetActive(true);
            t.DOKill();
            t.DOFade(1f, 0);
        }
    }

    public void Hide()
    {
        foreach (var t in GetComponentsInChildren<Graphic>())
        {
            // Fade out all graphics and then hide them
            t.DOKill();
            t.DOFade(0f, 1f).OnComplete(() => t.gameObject.SetActive(false));
        }
    }
}