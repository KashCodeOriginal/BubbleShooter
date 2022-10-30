using UnityEngine;

public class BallInstanceCreater : MonoBehaviour
{
    private BallSpawner _ballSpawner;
    private ObjectInput _objectInput;

    private GameObject _cannon;

    private void Start()
    {
        _ballSpawner = GetComponent<BallSpawner>();
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

        if (ball.TryGetComponent(out MovingBallCollides ballCollides))
        {
            _objectInput.OnRotateEnded += CreateBall;
        }
    }

}
