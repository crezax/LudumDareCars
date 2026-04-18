using UnityEngine;

public class ColliderEventsDelegate : MonoBehaviour
{
    public delegate void ColliderEvent(Collider collider);
    public ColliderEvent OnTriggerEnterEvent;
    public ColliderEvent OnTriggerExitEvent;

    protected void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }

    protected void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }
}