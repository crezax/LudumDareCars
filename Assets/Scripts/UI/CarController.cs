using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private bool boostSignalUsed = false;
    [SerializeField]
    private Button turnLeftButton;
    [SerializeField]
    private Button moveForwardButton;
    [SerializeField]
    private Button turnRightButton;
    [SerializeField]
    private Button boostButton;
    [SerializeField]
    private Button boostSignalButton;

    protected void Start()
    {
        if (GameManager.Instance.CurrentLevelIndex < 4)
        {
            boostSignalButton.gameObject.SetActive(false);
        }
        if (GameManager.Instance.CurrentLevelIndex < 7)
        {
            boostButton.gameObject.SetActive(false);
        }
    }

    protected void Update()
    {
        if (TurnManager.Instance.TurnInProgress)
        {
            turnLeftButton.interactable = false;
            moveForwardButton.interactable = false;
            turnRightButton.interactable = false;
            boostButton.interactable = false;
            boostSignalButton.interactable = false;
        }
        else
        {
            turnLeftButton.interactable = true;
            moveForwardButton.interactable = true;
            turnRightButton.interactable = true;
            boostButton.interactable = true;
            boostSignalButton.interactable = !boostSignalUsed;
        }
    }

    public void MoveForward()
    {
        if (TurnManager.Instance.TurnInProgress) return;
        Cars.ForEach(car => car.CurrentState = Car.State.Move);
        TurnManager.Instance.PerformTurn();
    }

    public void MoveRight()
    {
        if (TurnManager.Instance.TurnInProgress) return;
        Cars.ForEach(car => car.CurrentState = Car.State.TurnRight);
        TurnManager.Instance.PerformTurn();
    }

    public void MoveLeft()
    {
        if (TurnManager.Instance.TurnInProgress) return;
        Cars.ForEach(car => car.CurrentState = Car.State.TurnLeft);
        TurnManager.Instance.PerformTurn();
    }

    public void Boost()
    {
        if (TurnManager.Instance.TurnInProgress) return;
        Cars.ForEach(car => car.CurrentState = Car.State.Boost);
        TurnManager.Instance.PerformTurn();
    }

    public void BoostSignal()
    {
        if (TurnManager.Instance.TurnInProgress) return;
        if (boostSignalUsed) return;
        boostSignalUsed = true;
        Cars.ForEach(car => car.BoostSignal());
    }

    private List<Car> Cars
    {
        get => new(FindObjectsByType<Car>(FindObjectsSortMode.None));
    }
}