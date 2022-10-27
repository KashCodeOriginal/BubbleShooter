using UnityEngine;

public interface IRotatable
{
    public void Construct(Transform rotatableObject, float rotationSpeed);
    public void Rotate(Vector2 currentMousePosition);
}
