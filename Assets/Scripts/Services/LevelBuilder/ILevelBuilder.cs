public interface ILevelBuilder
{
    public void SetLevelGenerationWay(bool isLevelRandom, Cell[,] generatedLevel);
    public void BuildRandomLevel();
    public void BuildGeneratedLevel();
    public void UpdateCurrentLevel();
}
