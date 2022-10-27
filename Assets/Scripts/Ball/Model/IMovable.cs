using UnityEngine;

public interface IMovable
{
    public float Speed { get; }

    public void Move();
}

public class BallMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed;

    public float Speed => _speed;
    
    public void Move()
    {
        throw new System.NotImplementedException();
    }
}
