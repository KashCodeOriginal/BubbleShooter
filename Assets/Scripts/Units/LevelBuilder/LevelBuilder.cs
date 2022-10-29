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

    [SerializeField] private float _distance;
    [SerializeField] private Vector3 _centerPosition;
    

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
            for (int y = 0; y < CellsMatrixWatcher.COLUMNS_COUNT; y++)
            {
                var cell = cells[x, y];

                var ballType = GetBallType(cell.CellType);

                var cornerPosition = GetCornerPosition(_centerPosition, x, y, _distance);

                var targetPosition = GetSpawnPosition(cornerPosition, x, y, _distance);
                
                if (ballType != 0)
                {
                    var ballInstance = await _ballSpawner.CreateDecoratableBall(Vector2.zero, ballType);

                    ballInstance.transform.position = targetPosition;

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
    
    private Vector2 GetCornerPosition(Vector2 centerPosition, int row, int column, float distance)
    {
        return centerPosition +
               Vector2.down * column * distance * 0.25f +
               Vector2.left * row * distance * 0.25f;
    }
    
    private Vector2 GetSpawnPosition(Vector2 cornerPosition,int row, int column, float distance)
    {
        return cornerPosition + 
               Vector2.up * column * distance + 
               Vector2.right * row * distance;
    }
}
