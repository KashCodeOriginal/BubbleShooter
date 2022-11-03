using Zenject;
using UnityEngine;
using System.Collections;
using KasherOriginal.Factories.BallFactory;

public class BallDestroyHandler : MonoBehaviour
{
    [Inject]
    public void Construct(IBallsFactory ballsFactory)
    {
        _ballsFactory = ballsFactory;
    }
    
    private IDestroyable _destroyable;
    private IBallsFactory _ballsFactory;

    private void Start()
    {
        _destroyable = GetComponent<IDestroyable>();

        _destroyable.OnBallDestroyed += DestroyInstance;
    }

    private void DestroyInstance()
    {
        StartCoroutine(WaitForCallback());
    }

    private void OnDisable()
    {
        _destroyable.OnBallDestroyed -= DestroyInstance;
    }

    private IEnumerator WaitForCallback()
    {
        yield return new WaitForSeconds(0.01f);
        _ballsFactory.DestroyInstance(gameObject);
    }
}
