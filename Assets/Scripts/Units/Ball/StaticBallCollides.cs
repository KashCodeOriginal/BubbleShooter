using Zenject;
using UnityEngine;

public class StaticBallCollides : MonoBehaviour
{
    [Inject]
    public void Construct(ICellsMatrixWatcher cellsMatrixWatcher)
    {
        _cellsMatrixWatcher = cellsMatrixWatcher;
    }
    
    [SerializeField] private BoxCollider2D _downSideCollider;
    [SerializeField] private BoxCollider2D _upSideCollider;
    [SerializeField] private BoxCollider2D _leftSideCollider;
    [SerializeField] private BoxCollider2D _rightSideCollider;

    private bool _ableToCollide = true;

    private ICellsMatrixWatcher _cellsMatrixWatcher;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_ableToCollide)
        {
            if (col.gameObject.CompareTag("MovingBall"))
            {
                if (col.collider.IsTouching(_downSideCollider))
                {
                    ProcessBallConnection(BallConnectionType.Down, gameObject.GetComponent<BallSpriteBehavior>(), col.gameObject.GetComponent<BallSpriteBehavior>());
                    return;
                }
                if (col.collider.IsTouching(_upSideCollider))
                {
                    ProcessBallConnection(BallConnectionType.Up, gameObject.GetComponent<BallSpriteBehavior>(), col.gameObject.GetComponent<BallSpriteBehavior>());
                    return;
                }
                if (col.collider.IsTouching(_leftSideCollider))
                {
                    ProcessBallConnection(BallConnectionType.Left, gameObject.GetComponent<BallSpriteBehavior>(), col.gameObject.GetComponent<BallSpriteBehavior>());
                    return;
                }
                if (col.collider.IsTouching(_rightSideCollider))
                {
                    ProcessBallConnection(BallConnectionType.Right, gameObject.GetComponent<BallSpriteBehavior>(), col.gameObject.GetComponent<BallSpriteBehavior>());
                    return;
                }
            }
        }
    }

    private void ProcessBallConnection(BallConnectionType connectionType, BallSpriteBehavior originalBallSpriteBehavior, BallSpriteBehavior shootedBallSpriteBehavior)
    {
        _cellsMatrixWatcher.ProcessBallConnection(connectionType, originalBallSpriteBehavior, shootedBallSpriteBehavior);
    }
}