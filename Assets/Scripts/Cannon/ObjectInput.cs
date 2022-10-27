using UnityEngine;
using UnityEngine.Events;

public class ObjectInput : MonoBehaviour
{
    public event UnityAction OnRotateEnded;
    
    private IRotatable _rotatable;

    private Vector2 _currentMousePosition;

    public Vector2 CurrentMousePosition => _currentMousePosition;

    private void Start()
    {
        _rotatable = GetComponent<IRotatable>();
    }

    private void OnMouseDrag()
    {
        Vector2 mouse = Input.mousePosition;

        _currentMousePosition = Camera.main.ScreenToWorldPoint(mouse);
        
        _rotatable.Rotate(_currentMousePosition);
    }

    private void OnMouseUp()
    {
        OnRotateEnded?.Invoke();
    }
}
