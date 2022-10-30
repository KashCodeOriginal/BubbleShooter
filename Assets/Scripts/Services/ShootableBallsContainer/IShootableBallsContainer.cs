using System.Collections.Generic;

public interface IShootableBallsContainer
{
    public IReadOnlyList<Ball> Balls { get; }

    public void RegisterBall(Ball instance);
    public Ball GetNextBall();
    public void DeleteBall(Ball instance);
}