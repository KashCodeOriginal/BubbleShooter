using System;
using Zenject;
using UnityEngine;

public class LevelBuilder : MonoBehaviour, ILevelBuilder
{
    [Inject]
    public void Construct(ICellsMatrixWatcher cellsMatrixWatcher, IBallsInstancesWatcher ballsInstancesWatcher)
    {
        _cellsMatrixWatcher = cellsMatrixWatcher;
        _ballsInstancesWatcher = ballsInstancesWatcher;
    }

    [SerializeField] private float _distance;
    [SerializeField] private Vector3 _centerPosition;

    private ICellsMatrixWatcher _cellsMatrixWatcher;
    private BallSpawner _ballSpawner;
    private IBallsInstancesWatcher _ballsInstancesWatcher;

    private void Start()
    {
        _ballSpawner = FindObjectOfType<BallSpawner>();
        
        _cellsMatrixWatcher.CreateRandomField();
        
        BuildRandomLevel();
    }

    private void Update()
    {
        UpdateCurrentLevel();
    }

    public void BuildRandomLevel()
    {
        _ballsInstancesWatcher.DestroyAllInstances();
        _cellsMatrixWatcher.CreateRandomField();
        _cellsMatrixWatcher.SetLevelField(_cellsMatrixWatcher.Cells);
        BuildLevel(_cellsMatrixWatcher.Cells);
    }

    public void UpdateCurrentLevel()
    {
        _ballsInstancesWatcher.DestroyAllInstances();
        BuildLevel(_cellsMatrixWatcher.Cells);
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

    private async void BuildLevel(Cell[,] cells)
    {
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
                    var ballInstance = await _ballSpawner.CreateStaticBall(Vector2.zero, ballType);

                    ballInstance.transform.position = targetPosition;

                    if (ballInstance.TryGetComponent(out BallSpriteBehavior ball))
                    {
                        _cellsMatrixWatcher.ChangeCellInfo(ballType, ball, x, y);
                        _ballsInstancesWatcher.Register(ballInstance);
                    }
                }
            }
        }
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
