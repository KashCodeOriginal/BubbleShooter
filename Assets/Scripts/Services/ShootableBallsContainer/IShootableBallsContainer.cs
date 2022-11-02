using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IShootableBallsContainer
{
    public event UnityAction<Color> NextBallColorChanged;
    public IReadOnlyList<Ball> Balls { get; }

    public void RegisterBall(Ball instance);
    public bool CanTakeCurrentBall();
    public Ball GetCurrentBall();
    public void DeleteBall(Ball instance);
    public void DeleteAllBalls();
}