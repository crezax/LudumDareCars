using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;

    public static TurnManager Instance
    {
        get => instance;
    }

    private readonly List<TurnBasedBehaviour> turnBasedBehaviours = new();
    private readonly List<TurnBasedBehaviour> turnBasedBehavioursToUnregister = new();
    private int turnNumber = 0;

    public int TurnNumber
    {
        get => turnNumber;
    }

    private bool turnLogicInProgress = false;
    private bool CarAnimationsInProgress
    {
        get
        {
            var cars = FindObjectsByType<Car>(FindObjectsSortMode.None);
            if (cars.Length == 0)
            {
                return false;
            }
            var tweens = cars
                .Select(car => DOTween.TweensByTarget(car.transform))
                .Where(carTweens => carTweens != null)
                .SelectMany(carTweens => carTweens)
                .Where(tween => tween != null)
                .ToList();

            return tweens.Count > 0;
        }
    }
    public bool TurnInProgress
    {
        get
        {
            return turnLogicInProgress || CarAnimationsInProgress;
        }
    }
    // private static readonly float TURN_DURATION = 2f;
    // private float timeTillNextTurn = TURN_DURATION;

    protected void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    /*protected void Update()
    {
        if (timeTillNextTurn > 0)
        {
            timeTillNextTurn -= Time.deltaTime;
        }
        else
        {
            timeTillNextTurn = TURN_DURATION;
            foreach (TurnBasedBehaviour turnBasedBehaviour in turnBasedBehaviours)
            {
                turnBasedBehaviour.BeforeTurn();
            }
            foreach (TurnBasedBehaviour turnBasedBehaviour in turnBasedBehaviours)
            {
                turnBasedBehaviour.DuringTurn();
            }
            foreach (TurnBasedBehaviour turnBasedBehaviour in turnBasedBehaviours)
            {
                turnBasedBehaviour.AfterTurn();
            }
        }
    }*/

    public void Register(TurnBasedBehaviour turnBasedBehaviour)
    {
        turnBasedBehaviours.Add(turnBasedBehaviour);
    }

    public void Unregister(TurnBasedBehaviour turnBasedBehaviour)
    {
        turnBasedBehavioursToUnregister.Add(turnBasedBehaviour);
    }

    public void PerformTurn()
    {
        turnNumber++;
        StartCoroutine(PerformTurnCoroutine());
    }

    private System.Collections.IEnumerator PerformTurnCoroutine()
    {
        turnLogicInProgress = true;
        foreach (TurnBasedBehaviour turnBasedBehaviour in turnBasedBehaviours)
        {
            if (turnBasedBehavioursToUnregister.Contains(turnBasedBehaviour)) continue;
            turnBasedBehaviour.BeforeTurn();
        }
        foreach (TurnBasedBehaviour turnBasedBehaviour in turnBasedBehaviours)
        {
            if (turnBasedBehavioursToUnregister.Contains(turnBasedBehaviour)) continue;
            turnBasedBehaviour.DuringTurn();
        }
        yield return new WaitForSeconds(1f);
        while (CarAnimationsInProgress)
        {
            yield return null;
        }
        foreach (TurnBasedBehaviour turnBasedBehaviour in turnBasedBehaviours)
        {
            if (turnBasedBehavioursToUnregister.Contains(turnBasedBehaviour)) continue;
            turnBasedBehaviour.AfterTurn();
        }

        foreach (TurnBasedBehaviour turnBasedBehaviour in turnBasedBehavioursToUnregister)
        {
            turnBasedBehaviours.Remove(turnBasedBehaviour);
        }
        turnBasedBehavioursToUnregister.Clear();

        turnLogicInProgress = false;
    }
}