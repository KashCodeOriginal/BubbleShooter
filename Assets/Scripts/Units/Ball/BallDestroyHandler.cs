using Zenject;
using UnityEngine;
using System.Collections;
using KasherOriginal.Factories.BallFactory;

public class BallDestroyHandler : MonoBehaviour
{
    [Inject]
    public void Construct(IBallsInstancesWatcher ballsInstancesWatcher, IBallsFactory ballsFactory)
    {
        _ballsInstancesWatcher = ballsInstancesWatcher;
        _ballsFactory = ballsFactory;
    }
    
    private IDestroyable _destroyable;
    private IBallsFactory _ballsFactory;
    private IBallsInstancesWatcher _ballsInstancesWatcher;

    private void Start()
    {
        _destroyable = GetComponent<IDestroyable>();

        _destroyable.OnBallDestroyed += DestroyInstance;
    }

    private void DestroyInstance()
    {
        _ballsFactory.DestroyInstance(gameObject);
    }

    private void OnDisable()
    {
        _destroyable.OnBallDestroyed -= DestroyInstance;
    }
}
