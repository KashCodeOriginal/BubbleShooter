public interface ILevelBuilder
{
    public void BuildRandomLevel();
    public void BuildGeneratedLevel(Cell[,] generatedLevel);
    public void UpdateCurrentLevel();
}
