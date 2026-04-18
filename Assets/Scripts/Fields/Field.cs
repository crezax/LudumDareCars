using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private Car car;
    public Car Car
    {
        get => car;
        set
        {
            car = value;
            CarEntered?.Invoke(car);
        }
    }

    public delegate void CarEnteredHandler(Car car);
    public event CarEnteredHandler CarEntered;

    public Field GetNeighbour(Direction direction)
    {
        Vector2Int offset = direction switch
        {
            Direction.Up => new Vector2Int(0, 2),
            Direction.Right => new Vector2Int(2, 0),
            Direction.Down => new Vector2Int(0, -2),
            Direction.Left => new Vector2Int(-2, 0),
            _ => throw new System.ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        Vector3 neighbourPosition = transform.position + new Vector3(offset.x, offset.y, 0);
        Collider[] colliders = Physics.OverlapSphere(neighbourPosition, 0.1f);
        foreach (Collider collider in colliders)
        {
            Field neighbourField = collider.GetComponent<Field>();
            if (neighbourField != null)
            {
                return neighbourField;
            }
        }
        return null;
    }
}