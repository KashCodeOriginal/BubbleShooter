using UnityEngine;

public interface IMovable
{
    public Vector2 TargetDirection { get; }
    public void Move();
    public void SetMovingDirection(Vector2 direction);
}