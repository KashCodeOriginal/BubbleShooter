using System.Collections.Generic;

public enum BallTypeBehavior
{
    Red,
    Blue,
    Green,
    Yellow
}

public enum CellTypeBehavior
{
    R,
    B,
    G,
    Y,
    E
}

public class BallTypesRelation
{
    private Dictionary<BallTypeBehavior, CellTypeBehavior> _ballTypeRelation =
        new Dictionary<BallTypeBehavior, CellTypeBehavior>()
        {
            {BallTypeBehavior.Red, CellTypeBehavior.R},
            {BallTypeBehavior.Blue, CellTypeBehavior.B},
            {BallTypeBehavior.Green, CellTypeBehavior.G},
            {BallTypeBehavior.Yellow, CellTypeBehavior.Y}
        };

    public CellTypeBehavior GetCellTypeFromBallType(BallTypeBehavior ballTypeBehavior)
    {
        return _ballTypeRelation[ballTypeBehavior];
    }
}

