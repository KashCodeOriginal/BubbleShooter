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
        new Cell(CellTypeBehavior.B, null, 1, 1),
        new Cell(CellTypeBehavior.G, null, 2, 2)
    };

    public Cell[][] Levels => _levels;
}
