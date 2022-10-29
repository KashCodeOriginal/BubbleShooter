using Zenject;
using UnityEngine;
using KasherOriginal.Factories.BallFactory;

public class LevelBuilder : MonoBehaviour, ILevelBuilder
{
    [Inject]
    public void Construct(IBallsFactory ballsFactory, ICellsMatrixWatcher cellsMatrixWatcher)
    {
        _ballsFactory = ballsFactory;
        _cellsMatrixWatcher = cellsMatrixWatcher;
    }

    private IBallsFactory _ballsFactory;
    private ICellsMatrixWatcher _cellsMatrixWatcher;
    private BallSpawner _ballSpawner;

    private void Start()
    {
        _ballSpawner = FindObjectOfType<BallSpawner>();
        
        _cellsMatrixWatcher.CreateEmptyFieldOfCells();
        
        BuildLevel(_cellsMatrixWatcher.Cells);
    }

    public async void BuildLevel(Cell[,] cells)
    {
        _cellsMatrixWatcher.SetLevelField(cells);
        
        for (int x = 0; x < CellsMatrixWatcher.ROWS_COUNT; x++)
        {
            for (int y = 0; y < CellsMatrixWatcher.ROWS_COUNT; y++)
            {
                var cell = cells[x, y];

                var ballType = GetBallType(cell.CellType);

                if (ballType != 0)
                {
                    var ballInstance = await _ballSpawner.CreateDecoratableBall(Vector2.zero, ballType);

                    if (ballInstance.TryGetComponent(out Ball ball))
                    {
                        _cellsMatrixWatcher.ChangeCellInfo(ballType, ball, x, y);
                    }
                }
            }
        }
    }

    private BallTypeBehavior GetBallType(CellTypeBehavior cellType)
    {
        switch (cellType)
        {
            case CellTypeBehavior.R:
                return BallTypeBehavior.Red;
            case CellTypeBehavior.B:
                return BallTypeBehavior.Blue;
            case CellTypeBehavior.G:
                return BallTypeBehavior.Green;
            case CellTypeBehavior.Y:
                return BallTypeBehavior.Yellow;
            case CellTypeBehavior.E:
                return BallTypeBehavior.Yellow; // Check
        }

        return 0;
    }
}
