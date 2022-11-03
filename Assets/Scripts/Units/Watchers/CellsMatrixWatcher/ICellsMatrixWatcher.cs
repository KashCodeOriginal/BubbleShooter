using System;

public interface ICellsMatrixWatcher
{
    public event Action BallOutOfBorder;
    public event Action PlayerWonGame;
    public Cell[,] Cells { get; }
    public void CreateRandomField();
    public void SetLevelField(Cell[,] levelField);
    public void ChangeCellInfo(BallTypeBehavior ballType, BallSpriteBehavior ballSpriteBehavior, int positionX, int positionY);
    public void ProcessBallConnection(BallConnectionType connectionType, BallSpriteBehavior originalBallSpriteBehavior, BallSpriteBehavior shootedBallSpriteBehavior);
}