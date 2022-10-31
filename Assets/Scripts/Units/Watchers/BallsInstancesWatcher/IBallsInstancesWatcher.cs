using UnityEngine;
using System.Collections.Generic;

public interface IBallsInstancesWatcher
{
    public List<GameObject> Instances { get; }
    public void Register(GameObject instance);
    public void DestroyInstance(GameObject instance);
    public void DestroyAllInstances();
}