public class Cell
{
    public Cell(CellTypeBehavior cellType, BallSpriteBehavior ballSpriteBehavior, int xPosition, int yPosition)
    {
        _cellType = cellType;
        _ballSpriteBehavior = ballSpriteBehavior;
        _xPosition = xPosition;
        _yPosition = yPosition;
    }

    public void SetCellToVisited()
    {
        _isVisited = true;
    }
    
    
    private CellTypeBehavior _cellType;
    private BallSpriteBehavior _ballSpriteBehavior;

    private int _xPosition;
    private int _yPosition;
    
    private bool _isVisited;

    public CellTypeBehavior CellType => _cellType;
    public BallSpriteBehavior BallSpriteBehavior => _ballSpriteBehavior;
    public int XPosition => _xPosition;
    public int YPosition => _yPosition;
    public bool IsVisited => _isVisited;
}