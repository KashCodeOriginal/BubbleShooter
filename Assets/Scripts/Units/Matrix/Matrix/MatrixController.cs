using UnityEngine;
using Zenject;

public class MatrixController : MonoBehaviour
{
    [Inject]
    public void Construct(ICellsMatrixWatcher cellsMatrixWatcher)
    {
        _cellsMatrixWatcher = cellsMatrixWatcher;
    }

    private ICellsMatrixWatcher _cellsMatrixWatcher;
}
