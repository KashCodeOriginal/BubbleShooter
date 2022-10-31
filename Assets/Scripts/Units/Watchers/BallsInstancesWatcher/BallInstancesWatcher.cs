using UnityEngine;
using System.Collections.Generic;
using KasherOriginal.Factories.BallFactory;

public class BallInstancesWatcher : IBallsInstancesWatcher
{
    public BallInstancesWatcher(IBallsFactory ballsFactory)
    {
        _ballsFactory = ballsFactory;
    }

    private readonly IBallsFactory _ballsFactory;
    
    private List<GameObject> _instances = new List<GameObject>();

    public List<GameObject> Instances
    {
        get => _instances;
    }
    
    public void Register(GameObject instance)
    {
        _instances.Add(instance);
    }

    public void DestroyInstance(GameObject instance)
    {
        _ballsFactory.DestroyInstance(instance);
    }

    public void DestroyAllInstances()
    {
        _ballsFactory.DestroyAllInstances(_instances);
    }
}