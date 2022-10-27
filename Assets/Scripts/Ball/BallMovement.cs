using System;
using KasherOriginal.Settings;
using UnityEngine;
using Zenject;

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

    private void Start()
    {
        Speed = _gameSettings.BallMovementSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }
    public void SetMovingDirection(Vector2 direction)
    {
        TargetDirection = direction;
    }

    public void Move()
    {
        if (TargetDirection != Vector2.zero)
        {
            transform.Translate(TargetDirection * Speed * Time.deltaTime);  
        }
    }
}