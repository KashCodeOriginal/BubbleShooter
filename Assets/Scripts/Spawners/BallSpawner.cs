using Zenject;
using UnityEngine;
using System.Threading.Tasks;
using KasherOriginal.Factories.BallFactory;

public class BallSpawner : MonoBehaviour
{
    [Inject]
    public void Construct(IBallsFactory ballsFactory)
    {
        _ballsFactory = ballsFactory;
    }
    
    [SerializeField] private BallColorDecorator[] _ballColorDecorators;

    [SerializeField] private Transform _spawnPosition;

    private IBallsFactory _ballsFactory;

    public async Task<GameObject> CreateBall()
    {
        if (!isActiveAndEnabled)
        {
            return null;
        }

        GameObject ballInstance = await _ballsFactory.CreateInstance(_spawnPosition.position, _ballColorDecorators[GetRandomValue(0, _ballColorDecorators.Length)]);

        return ballInstance;
    }
    
    private int GetRandomValue(int startValue, int endValue)
    {
        int randomValue = Random.Range(startValue, endValue);
        return randomValue;
    }
}
