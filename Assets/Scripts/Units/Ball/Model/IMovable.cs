using UnityEngine;
using UnityEngine.Events;

public interface IMovable
{
    public Vector2 TargetDirection { get; }
    public void SetMovingDirection(Vector2 direction);
    public void SetUp(ObjectInput objectInput, GameObject startPoint);
}