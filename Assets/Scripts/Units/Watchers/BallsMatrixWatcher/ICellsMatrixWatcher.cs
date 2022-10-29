using UnityEngine;

public interface ICellsMatrixWatcher
{
    public Cell[,] Cells { get; }
    public void CreateEmptyFieldOfCells();
    public void SetLevelField(Cell[,] levelField);
    public void ChangeCellInfo(BallTypeBehavior ballType, Ball ball, int positionX, int positionY);
}

public class CellsMatrixWatcher : ICellsMatrixWatcher
{
    public CellsMatrixWatcher(IBallTypesRelation ballTypesRelation)
    {
        _ballTypesRelation = ballTypesRelation;
    }
    
    public const int ROWS_COUNT = 8;
    public const int COLUMNS_COUNT = 8;

    private IBallTypesRelation _ballTypesRelation;

    public Cell[,] Cells { get; private set; } = new Cell[ROWS_COUNT,COLUMNS_COUNT];

    public void CreateEmptyFieldOfCells()
    {
        for (int x = 0; x < ROWS_COUNT; x++)
        {
            for (int y = 0; y < COLUMNS_COUNT; y++)
            {
                Cell cell = new Cell(CellTypeBehavior.E, null, x, y);
                Cells[x, y] = cell;
            }
        }
    }

    public void SetLevelField(Cell[,] levelField)
    {
        Cells = levelField;
    }

    public void ChangeCellInfo(BallTypeBehavior ballType, Ball ball, int positionX, int positionY)
    {
        Cells[positionX, positionY] = new Cell(_ballTypesRelation.GetCellTypeFromBallType(ballType), ball, positionX, positionY);
    }
}