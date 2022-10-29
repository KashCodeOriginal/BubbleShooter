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
    
    public bool CanMove { get; private set; }
    public float Speed { get; private set; }

    private GameSettings _gameSettings;
    private ObjectInput _objectInput;
    private GameObject _cannon;
    private MovingMovingBallCollides _movingMovingBallCollides;

    private bool _canCreateNewBall;

    private void Start()
    {
        _movingMovingBallCollides = GetComponent<MovingMovingBallCollides>();
        
        _movingMovingBallCollides.OnBallDestroyed += CanCreateNewMovingMovingBall;
        
        CanMove = false;

        _canCreateNewBall = true;
        
        Speed = _gameSettings.BallMovementSpeed;
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

    public void TryMove()
    {
        if (CanMove)
        {
            transform.Translate(TargetDirection * Speed * Time.deltaTime);  
        }
    }

    private void StartBallMoving()
    {
        //gameObject.transform.SetParent(null);
        
        if (_canCreateNewBall)
        {
            TargetDirection = CreateBallMoveDirection();
            CanMove = true;

            _canCreateNewBall = false;
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

    private void CanCreateNewMovingMovingBall()
    {
        _canCreateNewBall = true;
    }

    private void OnDisable()
    {
        _objectInput.OnRotateEnded -= StartBallMoving;
        _movingMovingBallCollides.OnBallDestroyed -= CanCreateNewMovingMovingBall;
    }
}