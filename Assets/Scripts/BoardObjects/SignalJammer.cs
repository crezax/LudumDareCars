using DG.Tweening;
using UnityEngine;

public class SignalJammer : TurnBasedBehaviour
{
    [SerializeField]
    private ColliderEventsDelegate colliderEventsDelegate;

    protected void Awake()
    {
        colliderEventsDelegate.OnTriggerEnterEvent += OnTriggerEnter;
        colliderEventsDelegate.OnTriggerExitEvent += OnTriggerExit;
        transform.DOShakePosition(1f, 0.1f, 1, 90, false).SetLoops(-1); // shake indefinitely
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Car>(out var car))
        {
            car.JamSignal();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Car>(out var car))
        {
            car.UnjamSignal();
        }
    }
}