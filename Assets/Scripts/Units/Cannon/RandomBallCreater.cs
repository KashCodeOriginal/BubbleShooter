using UnityEngine;

public class RandomBallCreater : MonoBehaviour
{
    private BallSpawner _ballSpawner;
    private ObjectInput _objectInput;

    private GameObject _cannon;

    private void Start()
    {
        _ballSpawner = GetComponent<BallSpawner>();
        
        CreateBall();
    }

    public void Construct(ObjectInput objectInput, GameObject cannon)
    {
        _objectInput = objectInput;
        _cannon = cannon;
    }

    private async void CreateBall()
    {
        var ball = await _ballSpawner.CreateRandomDecoratableBall();
        
        //ball.transform.SetParent(gameObject.transform);

        if (ball.TryGetComponent(out IMovable movable))
        {
            movable.SetUp(_objectInput, _cannon);
        }

        if (ball.TryGetComponent(out MovingMovingBallCollides ballCollides))
        {
            ballCollides.OnBallDestroyed += CreateBall;
        }
    }

}
