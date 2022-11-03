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

    private int _collidesCount;

    private bool _canCollide;

    private void Start()
    {
        _movable = GetComponent<IMovable>();

        _canCollide = true;
        
        _collidesCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_canCollide)
        {
            if (col.gameObject.CompareTag("StaticBall") || _collidesCount >= _gameSettings.MaxBallWallsCollider)
            {
                _canCollide = false;
                OnBallDestroyed?.Invoke();
                return;
            }
            
            _collidesCount++;

            if (col.gameObject.CompareTag("Wall"))
            {
                _movable.SetMovingDirection(Vector2.Reflect(_movable.TargetDirection, col.contacts[0].normal));
            }
        }
    }
}