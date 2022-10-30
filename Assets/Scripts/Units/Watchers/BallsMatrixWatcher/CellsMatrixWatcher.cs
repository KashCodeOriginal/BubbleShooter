using KasherOriginal.Settings;
using Random = UnityEngine.Random;

public class CellsMatrixWatcher : ICellsMatrixWatcher
{
    public CellsMatrixWatcher(IBallTypesRelation ballTypesRelation, GameSettings gameSettings)
    {
        _ballTypesRelation = ballTypesRelation;
        _gameSettings = gameSettings;
    }
    
    public const int ROWS_COUNT = 12;
    public const int COLUMNS_COUNT = 18;
    
    private IBallTypesRelation _ballTypesRelation;
    private GameSettings _gameSettings;

    private CellTypeBehavior[] _cellTypes = new[]
    {
        CellTypeBehavior.R,
        CellTypeBehavior.G,
        CellTypeBehavior.B,
        CellTypeBehavior.Y,
        CellTypeBehavior.E
    };

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

    public void CreateRandomField()
    {
        CreateEmptyFieldOfCells();

        int randomBallsAmount = Random.Range(_gameSettings.MinBallsAmount, _gameSettings.MaxBallsAmount);

        for (int i = 0; i < randomBallsAmount;)
        {
            int randomRow = Random.Range(0, ROWS_COUNT);
            int randomColumn = Random.Range(0, COLUMNS_COUNT - 8);
            
            CellTypeBehavior randomCellType = _cellTypes[Random.Range(0, _cellTypes.Length)];

            if (randomCellType != CellTypeBehavior.E)
            {
                Cell cell = new Cell(randomCellType, null, randomRow, randomColumn);
                Cells[randomRow, randomColumn] = cell;
                i++;
            }
        }
    }

    public void SetLevelField(Cell[,] levelField)
    {
        Cells = levelField;
    }

    public void ChangeCellInfo(BallTypeBehavior ballType, BallSpriteBehavior ballSpriteBehavior, int positionX, int positionY)
    {
        Cells[positionX, positionY] = new Cell(_ballTypesRelation.GetCellTypeFromBallType(ballType), ballSpriteBehavior, positionX, positionY);
    }

    public void ProcessBallConnection(BallConnectionType connectionType, BallSpriteBehavior originalBallSpriteBehavior, BallSpriteBehavior shootedBallSpriteBehavior)
    {
        var originalCell = FindCellByBall(originalBallSpriteBehavior);

        var originalXPos = originalCell.XPosition;
        var originalYPos = originalCell.YPosition;

        var newCellType = _ballTypesRelation.GetCellTypeFromBallType(shootedBallSpriteBehavior.BallType);

        if (originalCell != null)
        {
            switch (connectionType)
            {
                case BallConnectionType.Up:
                    Cells[originalXPos, originalYPos - 1] = new Cell(newCellType, originalBallSpriteBehavior, originalXPos, originalYPos - 1);
                    break;
                case BallConnectionType.Down:
                    Cells[originalXPos, originalYPos + 1] = new Cell(newCellType, originalBallSpriteBehavior, originalXPos, originalYPos + 1);
                    break;
                case BallConnectionType.Left:
                    Cells[originalXPos - 1, originalYPos] = new Cell(newCellType, originalBallSpriteBehavior, originalXPos - 1, originalYPos);
                    break;
                case BallConnectionType.Right:
                    Cells[originalXPos + 1, originalYPos] = new Cell(newCellType, originalBallSpriteBehavior, originalXPos + 1, originalYPos);
                    break;
            }      
        }
    }

    private Cell FindCellByBall(BallSpriteBehavior ballSpriteBehavior)
    {
        foreach (var cell in Cells)
        {
            if (cell.BallSpriteBehavior == ballSpriteBehavior)
            {
                return cell;
            }
        }

        return null;
    }
}