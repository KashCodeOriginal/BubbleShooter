using System.Collections.Generic;

public class BallTypesRelation : IBallTypesRelation
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