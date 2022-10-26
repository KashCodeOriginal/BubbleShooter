using Zenject;
using UnityEngine;
using KasherOriginal.Factories.BallFactory;

public class BallSpawner : MonoBehaviour
{
    [Inject]
    public void Construct(IBallsFactory ballsFactory)
    {
        _ballsFactory = ballsFactory;
    }
    
    [SerializeField] private BallColorDecorator[] _ballColorDecorators;

    private IBallsFactory _ballsFactory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CreateBall(Vector3.one);
        }
    }

    public async void CreateBall(Vector3 position)
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

        GameObject ballInstance = await _ballsFactory.CreateInstance(position, _ballColorDecorators[GetRandomValue(0, _ballColorDecorators.Length)]);
    }
    
    private int GetRandomValue(int startValue, int endValue)
    {
        int randomValue = Random.Range(startValue, endValue);
        return randomValue;
    }
}
