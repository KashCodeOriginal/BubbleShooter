using UnityEngine;

public interface ICellsMatrixWatcher
{
    public Cell[,] Cells { get; }
    public void CreateEmptyFieldOfCells();
    public void CreateRandomField();
    public void SetLevelField(Cell[,] levelField);
    public void ChangeCellInfo(BallTypeBehavior ballType, Ball ball, int positionX, int positionY);
    public void ProcessBallConnection(BallConnectionType connectionType, Ball originalBall, Ball shootedBall);
}