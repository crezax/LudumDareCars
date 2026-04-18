using DG.Tweening;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private ColliderEventsDelegate colliderEventsDelegate;
    [SerializeField]
    private Animation animation;

    public enum Animation
    {
        None,
        RotateZ,
    }

    protected void Awake()
    {
        colliderEventsDelegate.OnTriggerEnterEvent += OnTriggerEnter;

        switch (animation)
        {
            case Animation.RotateZ:
                transform.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
                break;

        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Car>(out var car))
        {
            car.Die();
        }
    }
}