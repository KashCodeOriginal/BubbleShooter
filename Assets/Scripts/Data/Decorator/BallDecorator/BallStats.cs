using UnityEngine;

public struct BallStats
{
    public Color Color;
    public BallTypeBehavior BallTypeBehavior;

    public BallStats(Color color, BallTypeBehavior ballTypeBehavior)
    {
        Color = color;
        BallTypeBehavior = ballTypeBehavior;
    }
}
