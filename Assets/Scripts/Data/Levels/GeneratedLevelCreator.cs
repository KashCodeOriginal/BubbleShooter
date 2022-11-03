public class GeneratedLevelCreator : IGeneratedLevelCreator
{
    public Cell[,] CreateLevel(Cell[] cells)
    {
        Cell[,] generatedCell = new Cell[CellsMatrixWatcher.ROWS_COUNT, CellsMatrixWatcher.COLUMNS_COUNT];
        
        for (int x = 0; x < CellsMatrixWatcher.ROWS_COUNT; x++)
        {
            for (int y = 0; y < CellsMatrixWatcher.COLUMNS_COUNT; y++)
            {
                foreach (var cell in cells)
                {
                    if (cell.XPosition == x && cell.YPosition == y && cell.CellType != CellTypeBehavior.E)
                    {
                        generatedCell[x, y] = cell;
                        break;
                    }
                }

                generatedCell[x, y] = new Cell(CellTypeBehavior.E, null, x, y);

            }
        }

        return generatedCell;
    }
}