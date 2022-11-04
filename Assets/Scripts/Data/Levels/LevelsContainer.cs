public class LevelsContainer
{
    public LevelsContainer()
    {
        _levels = new []
        {
            _arrowsLevel
        };
    }

    private Cell[][] _levels;

    private Cell[] _arrowsLevel = new Cell[]
    {
        new(CellTypeBehavior.B, null, 1, 0), new(CellTypeBehavior.Y, null, 1, 1), new(CellTypeBehavior.R, null, 1, 2), new(CellTypeBehavior.G, null, 1, 3), new(CellTypeBehavior.G, null, 1, 4), new(CellTypeBehavior.G, null, 1, 5), new(CellTypeBehavior.G, null, 1, 6), new(CellTypeBehavior.R, null, 1, 7), new(CellTypeBehavior.Y, null, 1, 8), new(CellTypeBehavior.B, null, 1, 9), new(CellTypeBehavior.B, null, 2, 1), new(CellTypeBehavior.Y, null, 2, 2), new(CellTypeBehavior.R, null, 2, 3), new(CellTypeBehavior.G, null, 2, 4), new(CellTypeBehavior.G, null, 2, 5), new(CellTypeBehavior.R, null, 2, 6), new(CellTypeBehavior.Y, null, 2, 7), new(CellTypeBehavior.B, null, 2, 8), new(CellTypeBehavior.B, null, 3, 2), new(CellTypeBehavior.Y, null, 3, 3), new(CellTypeBehavior.R, null, 3, 4), new(CellTypeBehavior.R, null, 3, 5), new(CellTypeBehavior.Y, null, 3, 6), new(CellTypeBehavior.B, null, 3, 7), new(CellTypeBehavior.Y, null, 4, 0), new(CellTypeBehavior.B, null, 4, 3), new(CellTypeBehavior.Y, null, 4, 4), new(CellTypeBehavior.Y, null, 4, 5), new(CellTypeBehavior.B, null, 4, 6), new(CellTypeBehavior.Y, null, 4, 9), new(CellTypeBehavior.B, null, 5, 0), new(CellTypeBehavior.Y, null, 5, 1), new(CellTypeBehavior.B, null, 5, 4), new(CellTypeBehavior.B, null, 5, 5), new(CellTypeBehavior.Y, null, 5, 8), new(CellTypeBehavior.B, null, 5, 9), new(CellTypeBehavior.G, null, 6, 0), new(CellTypeBehavior.B, null, 6, 1), new(CellTypeBehavior.Y, null, 6, 2), new(CellTypeBehavior.Y, null, 6, 7), new(CellTypeBehavior.B, null, 6, 8), new(CellTypeBehavior.G, null, 6, 9), new(CellTypeBehavior.R, null, 7, 0), new(CellTypeBehavior.G, null, 7, 1), new(CellTypeBehavior.B, null, 7, 2), new(CellTypeBehavior.Y, null, 7, 3), new(CellTypeBehavior.Y, null, 7, 4), new(CellTypeBehavior.Y, null, 7, 5), new(CellTypeBehavior.Y, null, 7, 6), new(CellTypeBehavior.B, null, 7, 7), new(CellTypeBehavior.G, null, 7, 8), new(CellTypeBehavior.R, null, 7, 9), new(CellTypeBehavior.R, null, 8, 1), new(CellTypeBehavior.G, null, 8, 2), new(CellTypeBehavior.B, null, 8, 3), new(CellTypeBehavior.B, null, 8, 4), new(CellTypeBehavior.B, null, 8, 5), new(CellTypeBehavior.B, null, 8, 6), new(CellTypeBehavior.G, null, 8, 7), new(CellTypeBehavior.R, null, 8, 8), new(CellTypeBehavior.R, null, 9, 2), new(CellTypeBehavior.G, null, 9, 3), new(CellTypeBehavior.G, null, 9, 4), new(CellTypeBehavior.G, null, 9, 5), new(CellTypeBehavior.G, null, 9, 6), new(CellTypeBehavior.R, null, 9, 7), new(CellTypeBehavior.R, null, 10, 3), new(CellTypeBehavior.R, null, 10, 4), new(CellTypeBehavior.R, null, 10, 5), new(CellTypeBehavior.R, null, 10, 6), new(CellTypeBehavior.R, null, 11, 0), new(CellTypeBehavior.R, null, 11, 4), new(CellTypeBehavior.R, null, 11, 5), new(CellTypeBehavior.R, null, 11, 9), new(CellTypeBehavior.Y, null, 12, 0), new(CellTypeBehavior.R, null, 12, 1), new(CellTypeBehavior.R, null, 12, 8), new(CellTypeBehavior.Y, null, 12, 9), 
    };

    private Cell[] _squares = new Cell[]
    {

    };

    public Cell[][] Levels => _levels;
}
