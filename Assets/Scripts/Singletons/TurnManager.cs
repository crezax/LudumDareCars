using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;

    public static TurnManager Instance
    {
        get => instance;
    }

    private List<TurnBasedBehaviour> turnBasedBehaviours = new();
    private static readonly float TURN_DURATION = 2f;
    private float timeTillNextTurn = TURN_DURATION;

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

    protected void Update()
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
    }

    public void Register(TurnBasedBehaviour turnBasedBehaviour)
    {
        turnBasedBehaviours.Add(turnBasedBehaviour);
    }

    public void Unregister(TurnBasedBehaviour turnBasedBehaviour)
    {
        turnBasedBehaviours.Remove(turnBasedBehaviour);
    }
}