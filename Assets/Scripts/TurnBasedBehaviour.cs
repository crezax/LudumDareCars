using UnityEngine;

public class TurnBasedBehaviour : MonoBehaviour
{
    virtual public void BeforeTurn() { }
    virtual public void DuringTurn() { }
    virtual public void AfterTurn() { }

    protected void Start()
    {
        TurnManager.Instance.Register(this);
    }
}