using System;
using Zenject;
using UnityEngine;
using KasherOriginal.Factories.BallFactory;

public class LevelBuilder : MonoBehaviour, ILevelBuilder
{
    [Inject]
    public void Construct(IBallsFactory ballsFactory, ICellsMatrixWatcher cellsMatrixWatcher, IBallsInstancesWatcher ballsInstancesWatcher)
    {
        _ballsFactory = ballsFactory;
        _cellsMatrixWatcher = cellsMatrixWatcher;
        _ballsInstancesWatcher = ballsInstancesWatcher;
    }

    [SerializeField] private float _distance;
    [SerializeField] private Vector3 _centerPosition;

    private IBallsFactory _ballsFactory;
    private ICellsMatrixWatcher _cellsMatrixWatcher;
    private BallSpawner _ballSpawner;
    private IBallsInstancesWatcher _ballsInstancesWatcher;

    private void Start()
    {
        _ballSpawner = FindObjectOfType<BallSpawner>();
        
        _cellsMatrixWatcher.CreateRandomField();
        
        BuildLevel(_cellsMatrixWatcher.Cells);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BuildLevel(_cellsMatrixWatcher.Cells);
        }
    }

    public async void BuildLevel(Cell[,] cells)
    {
        _cellsMatrixWatcher.SetLevelField(cells);
        
        for (int x = 1; x < CellsMatrixWatcher.ROWS_COUNT - 1; x++)
        {
            for (int y = 0; y < CellsMatrixWatcher.COLUMNS_COUNT; y++)
            {
                var cell = cells[x, y];

                var ballType = GetBallType(cell.CellType);

                var cornerPosition = GetCornerPosition(_centerPosition, x, y, _distance);

                var targetPosition = GetSpawnPosition(cornerPosition, x, y, _distance);
                
                if (ballType != BallTypeBehavior.Empty)
                {
                    var ballInstance = await _ballSpawner.CreateDecoratableBall(Vector2.zero, ballType);

                    ballInstance.transform.position = targetPosition;

                    if (ballInstance.TryGetComponent(out Ball ball))
                    {
                        _cellsMatrixWatcher.ChangeCellInfo(ballType, ball, x, y);
                        _ballsInstancesWatcher.Register(ball);
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
                return BallTypeBehavior.Empty;
        }

        return BallTypeBehavior.Empty;
    }
    
    private Vector2 GetCornerPosition(Vector2 centerPosition, int row, int column, float distance)
    {
        return centerPosition +
               Vector2.up * column * distance * 0.25f +
               Vector2.left * row * distance * 0.25f;
    }
    
    private Vector2 GetSpawnPosition(Vector2 cornerPosition,int row, int column, float distance)
    {
        return cornerPosition + 
               Vector2.down * column * distance + 
               Vector2.right * row * distance;
    }
}
