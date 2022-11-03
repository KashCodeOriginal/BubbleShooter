using System;
using Cysharp.Threading.Tasks;
using KasherOriginal.Settings;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CellsMatrixWatcher : ICellsMatrixWatcher
{
    public CellsMatrixWatcher(IBallTypesRelation ballTypesRelation, GameSettings gameSettings,
        IBallsInstancesWatcher ballsInstancesWatcher)
    {
        _ballTypesRelation = ballTypesRelation;
        _gameSettings = gameSettings;
        _ballsInstancesWatcher = ballsInstancesWatcher;
    }

    public event Action BallOutOfBorder;
    public event Action PlayerWonGame;

    public const int ROWS_COUNT = 14;
    public const int COLUMNS_COUNT = 18;

    private readonly IBallTypesRelation _ballTypesRelation;
    private readonly GameSettings _gameSettings;
    private readonly IBallsInstancesWatcher _ballsInstancesWatcher;

    private bool _canProcessBall = true;

    private CellTypeBehavior[] _cellTypes = new[]
    {
        CellTypeBehavior.R,
        CellTypeBehavior.G,
        CellTypeBehavior.B,
        CellTypeBehavior.Y,
        CellTypeBehavior.E
    };

    public Cell[,] Cells { get; private set; } = new Cell[ROWS_COUNT, COLUMNS_COUNT];

    public void CreateRandomField()
    {
        var emptyCell = CreateEmptyFieldOfCells();

        Cells = emptyCell;

        int randomBallsAmount = Random.Range(_gameSettings.MinBallsAmount, _gameSettings.MaxBallsAmount);

        for (int i = 0; i < randomBallsAmount;)
        {
            int randomRow = Random.Range(1, ROWS_COUNT - 1);
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

    public void ChangeCellInfo(BallTypeBehavior ballType, BallSpriteBehavior ballSpriteBehavior, int positionX,
        int positionY)
    {
        Cells[positionX, positionY] = new Cell(_ballTypesRelation.GetCellTypeFromBallType(ballType), ballSpriteBehavior,
            positionX, positionY);
    }

    public void ProcessBallConnection(BallConnectionType connectionType, BallSpriteBehavior originalBallSpriteBehavior,
        BallSpriteBehavior shootedBallSpriteBehavior)
    {
        if (!_canProcessBall)
        {
            return;
        }

        _canProcessBall = false;
        
        DelayProcessTimer();
        
        var originalCell = FindCellByBall(originalBallSpriteBehavior);

        if (originalCell != null)
        {
            var originalXPos = originalCell.XPosition;
            var originalYPos = originalCell.YPosition;

            var newCellType = _ballTypesRelation.GetCellTypeFromBallType(shootedBallSpriteBehavior.BallType);

            switch (connectionType)
            {
                case BallConnectionType.Up:
                    Cells[originalXPos, originalYPos - 1] = new Cell(newCellType, originalBallSpriteBehavior,
                        originalXPos, originalYPos - 1);
                    FindAllNeighbors(shootedBallSpriteBehavior.BallType, originalXPos, originalYPos - 1);
                    break;
                case BallConnectionType.Down:
                    if (originalYPos + 1 >= COLUMNS_COUNT)
                    {
                        BallOutOfBorder?.Invoke();
                        return;
                    }

                    Cells[originalXPos, originalYPos + 1] = new Cell(newCellType, originalBallSpriteBehavior,
                        originalXPos, originalYPos + 1);
                    FindAllNeighbors(shootedBallSpriteBehavior.BallType, originalXPos, originalYPos + 1);
                    break;
                case BallConnectionType.Left:
                    Cells[originalXPos - 1, originalYPos] = new Cell(newCellType, originalBallSpriteBehavior,
                        originalXPos - 1, originalYPos);
                    FindAllNeighbors(shootedBallSpriteBehavior.BallType, originalXPos - 1, originalYPos);
                    break;
                case BallConnectionType.Right:
                    Cells[originalXPos + 1, originalYPos] = new Cell(newCellType, originalBallSpriteBehavior,
                        originalXPos + 1, originalYPos);
                    FindAllNeighbors(shootedBallSpriteBehavior.BallType, originalXPos + 1, originalYPos);
                    break;
            }
        }
    }


    public Cell[,] CreateEmptyFieldOfCells()
    {
        Cell[,] emptyField = new Cell[ROWS_COUNT,COLUMNS_COUNT];
        
        for (int x = 0; x < ROWS_COUNT; x++)
        {
            for (int y = 0; y < COLUMNS_COUNT; y++)
            {
                Cell cell = new Cell(CellTypeBehavior.E, null, x, y);
                emptyField[x, y] = cell;
            }
        }

        return emptyField;
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

    private void FindAllNeighbors(BallTypeBehavior ballType, int xBallPosition, int yBallPosition)
    {
        List<Cell> uncheckedCells = new List<Cell>();
        List<Cell> checkedCells = new List<Cell>();
        
        List<Cell> unconnectedCells = new List<Cell>();

        var selfBall = Cells[xBallPosition, yBallPosition];
        selfBall.SetCellToVisited();

        checkedCells.Add(selfBall);

        CheckForNearbyNeighbors(ballType, xBallPosition, yBallPosition, ref uncheckedCells);

        if (uncheckedCells.Count > 0)
        {
            CheckNeighbor();
        }

        void CheckNeighbor()
        {
            CheckForNearbyNeighbors(ballType, uncheckedCells[0].XPosition, uncheckedCells[0].YPosition,
                ref uncheckedCells);

            checkedCells.Add(uncheckedCells[0]);

            uncheckedCells.Remove(uncheckedCells[0]);

            if (uncheckedCells.Count > 0)
            {
                CheckNeighbor();
            }
        }

        if (checkedCells.Count >= 3)
        {
            foreach (Cell cell in checkedCells)
            {
                Cells[cell.XPosition, cell.YPosition] =
                    new Cell(CellTypeBehavior.E, null, cell.XPosition, cell.YPosition);
            }
        }

        CheckForNearbyNeighbors(ref unconnectedCells);
        
        if (_ballsInstancesWatcher.Instances.Count <= 1)
        {
            PlayerWonGame?.Invoke();
        }
    }

    private void CheckForNearbyNeighbors(BallTypeBehavior ballType, int xBallPosition, int yBallPosition,
        ref List<Cell> uncheckedNeighbors)
    {
        for (int x = Math.Max(0, xBallPosition - 1); x <= Math.Min(xBallPosition + 1, ROWS_COUNT); x++)
        {
            for (int y = Math.Max(0, yBallPosition - 1); y <= Math.Min(yBallPosition + 1, COLUMNS_COUNT); y++)
            {
                if (x != xBallPosition || y != yBallPosition)
                {
                    if (x < 0 || x >= ROWS_COUNT || y < 0 || y >= COLUMNS_COUNT)
                    {
                        break;
                    }
                    
                    if (Cells[x, y].CellType == _ballTypesRelation.GetCellTypeFromBallType(ballType) &&
                        Cells[x, y].IsVisited == false)
                    {
                        var neighborCell = Cells[x, y];
                        neighborCell.SetCellToVisited();

                        uncheckedNeighbors.Add(neighborCell);
                    }
                }
            }
        }
    }
    
    private void CheckForNearbyNeighbors(ref List<Cell> unconnectedCells)
    {
        foreach (var cell in Cells)
        {
            if (cell.CellType != CellTypeBehavior.E)
            {
                int neighbors = 0;
                
                for (int x = Math.Max(0, cell.XPosition - 1); x <= Math.Min(cell.XPosition + 1, ROWS_COUNT); x++)
                {
                    for (int y = Math.Max(0, cell.YPosition - 1); y <= Math.Min(cell.YPosition + 1, COLUMNS_COUNT); y++)
                    {
                        if (x != cell.XPosition || y != cell.YPosition)
                        {
                            if (x < 0 || x >= ROWS_COUNT || y < 0 || y >= COLUMNS_COUNT)
                            {
                                break;
                            }
                        
                            if (Cells[x, y].CellType != CellTypeBehavior.E)
                            {
                                neighbors++;
                            }
                        }
                    }
                }
                
                if (neighbors <= 0)
                {
                    unconnectedCells.Add(cell);
                }
            }
        }

        foreach (var cell in unconnectedCells)
        {
            Cells[cell.XPosition, cell.YPosition] =
                new Cell(CellTypeBehavior.E, null, cell.XPosition, cell.YPosition);
        }
    }
    
    private async void DelayProcessTimer()
    {
        await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
        _canProcessBall = true;
    } 
}