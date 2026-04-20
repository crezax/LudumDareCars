using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUiController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text turnCountText;
    [SerializeField]
    private TMP_Text bestTurnCountText;
    [SerializeField]
    private Button nextLevelButton;

    protected void OnEnable()
    {
        int bestTurnCount = PlayerPrefs.GetInt($"BestTurnCount_{GameManager.Instance.CurrentLevelIndex}", int.MaxValue);
        if (TurnManager.Instance.TurnNumber < bestTurnCount)
        {
            bestTurnCount = TurnManager.Instance.TurnNumber;
            PlayerPrefs.SetInt($"BestTurnCount_{GameManager.Instance.CurrentLevelIndex}", TurnManager.Instance.TurnNumber);
        }
        turnCountText.text = $"Turns: {TurnManager.Instance.TurnNumber}";
        bestTurnCountText.text = $"Best: {bestTurnCount}";
    }

    public void OnNextLevelButtonClicked()
    {
        GameManager.Instance.LoadNextLevel();
    }
}