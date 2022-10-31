using UnityEngine;
using System.Collections.Generic;

public class ShootableBallsContainer : IShootableBallsContainer
{
    private List<Ball> _balls = new List<Ball>();

    public IReadOnlyList<Ball> Balls
    {
        get => _balls;
    }

    public void RegisterBall(Ball instance)
    {
        _balls.Add(instance);
    }

    public Ball GetNextBall()
    {
        return _balls[0];
    }

    public void DeleteBall(Ball instance)
    {
        if (!_balls.Contains(instance))
        {
            Debug.LogError("There is no instance in list");
        }
        
        _balls.Remove(instance);
    }
}