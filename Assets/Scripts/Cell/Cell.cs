public class Cell
{
    public Cell(CellTypeBehavior cellType, Ball ball, int xPosition, int yPosition)
    {
        _cellType = cellType;
        _ball = ball;
        _xPosition = xPosition;
        _yPosition = yPosition;
    }
    
    private CellTypeBehavior _cellType;
    private Ball _ball;

    private int _xPosition;
    private int _yPosition;
    
    public CellTypeBehavior CellType => _cellType;
}