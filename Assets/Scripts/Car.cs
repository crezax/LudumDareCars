using DG.Tweening;
using UnityEngine;

public class Car : TurnBasedBehaviour
{
    public enum State
    {
        Move,
        TurnRight,
        TurnLeft,
        Boost
    }

    [SerializeField]
    private int jammerCount = 0;
    [SerializeField]
    private bool boostedSignal = false;
    private bool isAlive = true;
    public bool HasSignal
    {
        get => jammerCount == 0 || boostedSignal;
    }

    public void JamSignal()
    {
        jammerCount++;
        signalSymbol.SetActive(true);
    }

    public void UnjamSignal()
    {
        jammerCount = Mathf.Max(0, jammerCount - 1);
        if (HasSignal) signalSymbol.SetActive(false);
    }

    public void BoostSignal()
    {
        boostedSignal = true;
        signalSymbol.SetActive(false);
    }

    [SerializeField]
    private Direction currentDirection = Direction.Up;
    public Direction CurrentDirection
    {
        get => currentDirection;
        private set
        {
            currentDirection = value;
            transform.DORotate(new Vector3(0, 0, -(int)currentDirection * 90), 1f);
        }
    }

    private State currentState = State.Move;
    public State CurrentState
    {
        get => currentState;
        set
        {
            if (HasSignal)
            {
                currentState = value;
            }
        }
    }

    [SerializeField]
    private Field field;
    [SerializeField]
    private GameObject signalSymbol;
    public Field Field
    {
        get => field;
        private set
        {
            if (field != null) field.Car = null;
            field = value;
            if (field != null) field.Car = this;
        }
    }

    protected new void Start()
    {
        base.Start();
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Field>(out var field))
            {
                Field = field;
                break;
            }
        }
    }

    public override void DuringTurn()
    {
        Debug.Log($"Car {name} at {field.name} with direction {currentDirection} and state {currentState}");
        if (!HasSignal && CurrentState != State.Move && CurrentState != State.Boost)
        {
            currentState = State.Move;
            Debug.Log($"Car {name} at {field.name} with direction {currentDirection} and state {currentState}");
        }
        // CurrentState = actions[actionIndex];
        // Debug.Log($"Car at {field.transform.position} with direction {currentDirection} and state {currentState}");
        // actionIndex = (actionIndex + 1) % actions.Length;
        switch (CurrentState)
        {
            case State.Move:
                Move();
                break;
            case State.TurnRight:
                TurnRight();
                break;
            case State.TurnLeft:
                TurnLeft();
                break;
            case State.Boost:
                Boost();
                break;
        }
        Debug.Log($"Car {name} at {field.name} with direction {currentDirection}");
    }

    public override void AfterTurn()
    {
        base.AfterTurn();
        boostedSignal = false;
        signalSymbol.SetActive(!HasSignal);
    }

    // int actionIndex = 0;
    // State[] actions = new State[] { State.Move, State.TurnRight, State.Move, State.TurnRight, State.Boost, State.TurnRight, State.Boost, State.TurnRight, State.Boost, State.TurnLeft, State.TurnLeft, State.Move, State.TurnLeft, State.Move, State.TurnLeft };

    public void Die()
    {
        if (!isAlive) return;
        isAlive = false;
        transform.DOKill();
        Field = null;
        transform.DOShakePosition(1f, 0.5f, 5);
        Destroy(gameObject, 1f);
        GameManager.Instance.LevelFailed();
    }

    protected void OnDestroy()
    {
        Field = null;
        transform.DOKill();
        TurnManager.Instance.Unregister(this);
    }

    private void Move()
    {
        Field target = field.GetNeighbour(CurrentDirection);
        Debug.Log($"Car {name} trying to move from {field.name} to {target.name}");
        Field = target;
        transform.DOMove(new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z), 1f);
    }

    private void TurnRight()
    {
        CurrentDirection = (Direction)(((int)CurrentDirection + 1) % 4);
    }

    private void TurnLeft()
    {
        CurrentDirection = (Direction)(((int)CurrentDirection + 3) % 4);
    }

    private void Boost()
    {
        Field neighbour = field.GetNeighbour(CurrentDirection);
        Field target = neighbour.GetNeighbour(CurrentDirection);

        var moveSeq = DOTween.Sequence();

        moveSeq.Append(
            transform
                .DOMove(
                    new Vector3(
                        target.transform.position.x,
                        target.transform.position.y,
                        transform.position.z
                    ),
                    1f
                )
                .SetEase(Ease.InOutQuad)
        );

        moveSeq.InsertCallback(.5f, () =>
        {
            Field = neighbour;
        });

        moveSeq.OnComplete(() =>
        {
            Field = target;
        });
    }
}
