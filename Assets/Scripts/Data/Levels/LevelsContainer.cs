public class LevelsContainer
{
    public LevelsContainer()
    {
        _levels = new []
        {
            zigzagLevel
        };
    }

    private Cell[][] _levels;

    private Cell[] zigzagLevel = new Cell[]
    {
        new(CellTypeBehavior.B, null, 1, 1),
        new(CellTypeBehavior.G, null, 2, 2)
    };

    public Cell[][] Levels => _levels;
}
