using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private bool boostSignalUsed = false;

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