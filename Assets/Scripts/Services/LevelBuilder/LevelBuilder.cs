using System;
using Zenject;
using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour, ILevelBuilder
{
    [Inject]
    public void Construct(ICellsMatrixWatcher cellsMatrixWatcher, IBallsInstancesWatcher ballsInstancesWatcher)
    {
        _cellsMatrixWatcher = cellsMatrixWatcher;
        _ballsInstancesWatcher = ballsInstancesWatcher;
    }

    public event Action PlayerWonGame;

    [SerializeField] private float _updateTime;
    [SerializeField] private float _distance;
    [SerializeField] private Vector3 _centerPosition;

    private ICellsMatrixWatcher _cellsMatrixWatcher;
    private BallSpawner _ballSpawner;
    private IBallsInstancesWatcher _ballsInstancesWatcher;

    private Cell[,] _currentGeneratedLevel;
    private bool _isLevelRandom;


    private void Start()
    {
        _ballSpawner = FindObjectOfType<BallSpawner>();

        if (_isLevelRandom)
        {
            BuildRandomLevel();
            return;
        }
        
        BuildGeneratedLevel();
    }

    public void SetLevelGenerationWay(bool isLevelRandom, Cell[,] generatedLevel)
    {
        _isLevelRandom = isLevelRandom;

        _currentGeneratedLevel = generatedLevel;
    }

    public void BuildRandomLevel()
    {
        _ballsInstancesWatcher.DestroyAllInstances();
        _cellsMatrixWatcher.CreateRandomField();
        BuildLevel(_cellsMatrixWatcher.Cells);
    }

    public void BuildGeneratedLevel()
    {
        _ballsInstancesWatcher.DestroyAllInstances();
        _cellsMatrixWatcher.SetLevelField(_currentGeneratedLevel);
        BuildLevel(_cellsMatrixWatcher.Cells);
    }

    public void UpdateCurrentLevel()
    {
        StartCoroutine(BuildDelay());
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
        for (int x = 0; x < CellsMatrixWatcher.ROWS_COUNT; x++)
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

    private IEnumerator BuildDelay()
    {
        yield return new WaitForSeconds(_updateTime);
        _ballsInstancesWatcher.DestroyAllInstances();
        BuildLevel(_cellsMatrixWatcher.Cells);

        if (_ballsInstancesWatcher.Instances.Count <= 0)
        {
            PlayerWonGame?.Invoke();
        }
    }
}
