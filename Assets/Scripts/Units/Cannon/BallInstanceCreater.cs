using UnityEngine;

public class BallInstanceCreater : MonoBehaviour
{
    private BallSpawner _ballSpawner;
    private ObjectInput _objectInput;

    private GameObject _cannon;

    private ILevelBuilder _levelBuilder;

    private void Start()
    {
        _ballSpawner = GetComponent<BallSpawner>();

        _levelBuilder = GetComponent<ILevelBuilder>();
        
        _objectInput.OnRotateEnded += CreateBall;
    }

    public void Construct(ObjectInput objectInput, GameObject cannon)
    {
        _objectInput = objectInput;
        _cannon = cannon;
    }

    private async void CreateBall()
    {
        var ball = await _ballSpawner.CreateMovingBall();

        if (ball.TryGetComponent(out IMovable movable))
        {
            movable.SetUp(_objectInput, _cannon);
        }
        
        if (ball.TryGetComponent(out IDestroyable destroyable))
        {
            destroyable.OnBallDestroyed += UpdateCells;
        }
    }

    private void UpdateCells()
    {
        //_levelBuilder.UpdateCurrentLevel();
    }

    private void OnDisable()
    {
        _objectInput.OnRotateEnded -= CreateBall;
    }
}
