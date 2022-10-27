using UnityEngine;

public class BallCreater : MonoBehaviour
{
    private BallSpawner _ballSpawner;
    private ObjectInput _objectInput;

    private GameObject _cannon;

    private void Start()
    {
        _ballSpawner = GetComponent<BallSpawner>();
        
        _objectInput.OnRotateEnded += CreateBall;
    }

    public void Construct(ObjectInput objectInput, GameObject cannon)
    {
        _objectInput = objectInput;
        _cannon = cannon;
    }

    private async void CreateBall()
    {
        var ball = await _ballSpawner.CreateBall();

        if (ball.TryGetComponent(out IMovable movable))
        {
            movable.SetMovingDirection(CreateBallMoveDirection());
        }
    }

    private Vector2 CreateBallMoveDirection()
    {
        var mousePosition = _objectInput.CurrentMousePosition;
        
        var cannonPosition = _cannon.transform.position;
        var currentCannonPosition = new Vector2(cannonPosition.x, cannonPosition.y);

        var direction = mousePosition - currentCannonPosition;
        
        return direction.normalized;
    }
    
    private void OnDisable()
    {
        _objectInput.OnRotateEnded -= CreateBall;
    }
}
