using System;
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

    private bool _canCollide;

    private ICellsMatrixWatcher _cellsMatrixWatcher;

    private void Start()
    {
        _canCollide = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MovingBall"))
        {
            if (_canCollide)
            {
                if (col.collider.IsTouching(_downSideCollider))
                {
                    _canCollide = false;
                    ProcessBallConnection(BallConnectionType.Down, gameObject.GetComponent<BallSpriteBehavior>(), col.gameObject.GetComponent<BallSpriteBehavior>());
                    return;
                }
            }

            if (_canCollide)
            {
                if (col.collider.IsTouching(_upSideCollider))
                {
                    _canCollide = false;
                    ProcessBallConnection(BallConnectionType.Up, gameObject.GetComponent<BallSpriteBehavior>(), col.gameObject.GetComponent<BallSpriteBehavior>());
                    return;
                }
            }

            if (_canCollide)
            {
                if (col.collider.IsTouching(_leftSideCollider))
                {
                    _canCollide = false;
                    ProcessBallConnection(BallConnectionType.Left, gameObject.GetComponent<BallSpriteBehavior>(), col.gameObject.GetComponent<BallSpriteBehavior>());
                    return;
                }
            }

            if (_canCollide)
            {
                if (col.collider.IsTouching(_rightSideCollider))
                {
                    _canCollide = false;
                    ProcessBallConnection(BallConnectionType.Right, gameObject.GetComponent<BallSpriteBehavior>(), col.gameObject.GetComponent<BallSpriteBehavior>());
                }
            }
        }
    }

    private void ProcessBallConnection(BallConnectionType connectionType, BallSpriteBehavior originalBallSpriteBehavior, BallSpriteBehavior shootedBallSpriteBehavior)
    {
        _cellsMatrixWatcher.ProcessBallConnection(connectionType, originalBallSpriteBehavior, shootedBallSpriteBehavior);
    }
}