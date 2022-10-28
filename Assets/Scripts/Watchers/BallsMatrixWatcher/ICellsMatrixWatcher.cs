using UnityEngine;

public interface ICellsMatrixWatcher
{
    public void CreateEmptyFieldOfCells();
    public Cell[,] Cells { get; } 
}

public class CellsMatrixWatcher : ICellsMatrixWatcher
{
    private const int ROWS_COUNT = 8;
    private const int COLUMNS_COUNT = 8;

    public Cell[,] Cells { get; private set; } = new Cell[ROWS_COUNT,COLUMNS_COUNT];

    public void CreateEmptyFieldOfCells()
    {
        for (int x = 0; x <= ROWS_COUNT; x++)
        {
            for (int y = 0; y < COLUMNS_COUNT; y++)
            {
                Cell cell = new Cell(CellTypeBehavior.E, null, x, y);
                Cells[x, y] = cell;
            }
        }
    }
}