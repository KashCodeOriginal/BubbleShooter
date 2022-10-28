using Zenject;
using UnityEngine;
using UnityEngine.Events;
using KasherOriginal.Settings;

public class BallCollides : MonoBehaviour
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

    private void Start()
    {
        _movable = GetComponent<IMovable>();

        _collidesCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_collidesCount >= _gameSettings.MaxBallWallsCollider)
        {
            Destroy(gameObject);
                
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
