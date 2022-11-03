public class GeneratedLevelCreator : IGeneratedLevelCreator
{
    public GeneratedLevelCreator(ICellsMatrixWatcher cellsMatrixWatcher)
    {
        _cellsMatrixWatcher = cellsMatrixWatcher;
    }

    private readonly ICellsMatrixWatcher _cellsMatrixWatcher;
    
    public Cell[,] CreateLevel(Cell[] cells)
    {
        Cell[,] generatedCell = _cellsMatrixWatcher.CreateEmptyFieldOfCells();
        
        foreach (var cell in cells)
        {
            generatedCell[cell.XPosition, cell.YPosition] = new Cell(cell.CellType, null, cell.XPosition, cell.YPosition);
        }

        return generatedCell;
    }
}