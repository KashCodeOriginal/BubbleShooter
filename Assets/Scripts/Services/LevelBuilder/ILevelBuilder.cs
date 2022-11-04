using System;

public interface ILevelBuilder
{
    public event Action PlayerWonGame;
    public void SetLevelGenerationWay(bool isLevelRandom, Cell[,] generatedLevel);
    public void BuildRandomLevel();
    public void BuildGeneratedLevel();
    public void UpdateCurrentLevel();
}
