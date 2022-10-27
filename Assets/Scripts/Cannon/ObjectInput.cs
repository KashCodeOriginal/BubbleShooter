using UnityEngine;
using UnityEngine.Events;

public class ObjectInput : MonoBehaviour
{
    public event UnityAction OnRotateEnded;
    
    private ObjectRotate _objectRotate;

    private void Start()
    {
        _objectRotate = GetComponent<ObjectRotate>();
    }

    private void OnMouseDrag()
    {
        Vector2 mouse = Input.mousePosition;

        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(mouse);
        
        _objectRotate.Rotate(currentPosition);
    }

    private void OnMouseUp()
    {
        OnRotateEnded?.Invoke();
    }
}
