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

    public async Task<GameObject> CreateRandomDecoratableBall()
    {
        if (!isActiveAndEnabled)
        {
            return null;
        }
        
        GameObject ballInstance = await _ballsFactory.CreateDecoratableInstance(_spawnPosition.position, true, _ballColorDecorators[GetRandomValue(0, _ballColorDecorators.Length)]);

        return ballInstance;
    }
    
    public async Task<GameObject> CreateDecoratableBall(Vector2 position, BallTypeBehavior ballType)
    {
        if (!isActiveAndEnabled)
        {
            return null;
        }

        int decoratorPosition;

        for (decoratorPosition = 0; decoratorPosition < _ballColorDecorators.Length; decoratorPosition++)
        {
            if (_ballColorDecorators[decoratorPosition].BallType == ballType)
            {
                GameObject ballInstance = await _ballsFactory.CreateDecoratableInstance(position, false, _ballColorDecorators[decoratorPosition]);

                return ballInstance;
            }
        }

        return null;
    }
    
    
    
    private int GetRandomValue(int startValue, int endValue)
    {
        int randomValue = Random.Range(startValue, endValue);
        return randomValue;
    }
}
