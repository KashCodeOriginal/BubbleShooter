using Zenject;
using UnityEngine;
using KasherOriginal.Settings;

public class BallMovement : MonoBehaviour, IMovable
{
    [Inject]
    public void Construct(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }

    public Vector2 TargetDirection { get; private set; } = Vector2.zero;
    
    public float Speed { get; private set; }

    private GameSettings _gameSettings;
    private ObjectInput _objectInput;
    private GameObject _cannon;

    private void Start()
    {
        Speed = _gameSettings.BallMovementSpeed;
        
        StartBallMoving();
    }

    private void FixedUpdate()
    {
        TryMove();
    }
    
    public void SetMovingDirection(Vector2 direction)
    {
        TargetDirection = direction;
    }

    public void SetUp(ObjectInput objectInput, GameObject cannon)
    {
        _objectInput = objectInput;
        _cannon = cannon;

        _objectInput.OnRotateEnded += StartBallMoving;
    }

    private void TryMove()
    {
        if (TargetDirection != Vector2.zero)
        {
            transform.Translate(TargetDirection * Speed * Time.deltaTime); 
        }
    }

    private void StartBallMoving()
    {
        TargetDirection = CreateBallMoveDirection();
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
        _objectInput.OnRotateEnded -= StartBallMoving;
    }
}