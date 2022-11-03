using Zenject;
using UnityEngine;
using UnityEngine.Events;
using KasherOriginal.Settings;

public class MovingBallCollides : MonoBehaviour, IDestroyable
{
    [Inject]
    public void Construct(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }

    public event UnityAction OnBallDestroyed;
    
    private GameSettings _gameSettings;
    private IMovable _movable;

    private bool _canCollide;

    private int _collidesCount;


    private void Start()
    {
        _movable = GetComponent<IMovable>();

        _collidesCount = 0;

        _canCollide = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("StaticBall") || _collidesCount >= _gameSettings.MaxBallWallsCollider)
        {
            if (_canCollide)
            {
                OnBallDestroyed?.Invoke();
                _canCollide = false;
                return;
            }
        }
            
        _collidesCount++;

        if (col.gameObject.CompareTag("Wall"))
        {
            _movable.SetMovingDirection(Vector2.Reflect(_movable.TargetDirection, col.contacts[0].normal));
        }
    }
}