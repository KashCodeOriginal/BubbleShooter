using UnityEngine;

public class ObjectRotate : MonoBehaviour, IRotatable
{
    [SerializeField] private Transform _rotatableObject;

    [SerializeField] private float _rotationSpeed;

    public void Construct(Transform rotatableObject, float rotationSpeed)
    {
        _rotatableObject = rotatableObject;
        _rotationSpeed = rotationSpeed;
    }
    
    public void Rotate(Vector2 currentMousePosition)
    {
        var _rotatableObjectPosition = _rotatableObject.position;

        Vector2 direction = currentMousePosition - new Vector2(_rotatableObjectPosition.x, _rotatableObjectPosition.y);

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        
        _rotatableObject.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}
