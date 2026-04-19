using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Field))]
public class SpikesField : TurnBasedBehaviour
{
    private Field field;

    [SerializeField]
    private bool spikesActive = true;
    [SerializeField]
    private GameObject spikes;

    public bool SpikesActive
    {
        get => spikesActive;
        private set
        {
            spikesActive = value;
        }
    }

    protected void Awake()
    {
        field = GetComponent<Field>();
        field.CarEntered += OnCarEntered;
    }

    protected new void Start()
    {
        base.Start();
        if (SpikesActive)
        {
            spikes.transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            spikes.transform.localPosition = new Vector3(0, -0.5f, 0);
        }
    }

    public override void BeforeTurn()
    {
        base.BeforeTurn();
        SpikesActive = !SpikesActive;
        if (!SpikesActive) spikes.transform.DOLocalMoveY(-0.5f, 0.5f);
    }

    public override void AfterTurn()
    {
        base.AfterTurn();
        if (SpikesActive) spikes.transform.DOLocalMoveY(0f, 0.2f);
        if (field.Car != null && SpikesActive)
        {
            field.Car.Die();
        }
    }

    private void OnCarEntered(Car car)
    {
        if (SpikesActive)
        {
            car.Die();
        }
    }
}