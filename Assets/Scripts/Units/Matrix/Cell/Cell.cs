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

    public void SetSellToUnvisited()
    {
        _isVisited = false;
    }
    
    private CellTypeBehavior _cellType;
    private BallSpriteBehavior _ballSpriteBehavior;

    private bool _isVisited;

    private int _xPosition;
    private int _yPosition;
    
    public CellTypeBehavior CellType => _cellType;
    public BallSpriteBehavior BallSpriteBehavior => _ballSpriteBehavior;
    public int XPosition => _xPosition;
    public int YPosition => _yPosition;
    public bool IsVisited => _isVisited;
}