using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Field))]
public class TargetField : TurnBasedBehaviour
{
    private Field field;
    int carCounter = 0;

    protected void Awake()
    {
        field = GetComponent<Field>();
        field.CarEntered += OnCarEntered;
    }

    protected new void Start()
    {
        base.Start();
        carCounter = FindObjectsByType<Car>(FindObjectsSortMode.None).Length;
    }

    private void OnCarEntered(Car car)
    {
        TurnManager.Instance.Unregister(car);
        car.transform.DOScale(0, 1f);
        carCounter--;
    }

    public override void AfterTurn()
    {
        base.AfterTurn();
        if (field.Car) Destroy(field.Car.gameObject, 1f);
        if (carCounter == 0)
        {
            Debug.Log("All cars reached the target! You win!");
            GameManager.Instance.LevelCompleted();
        }
    }
}