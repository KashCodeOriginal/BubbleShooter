using System.Collections.Generic;

public interface IBallsInstancesWatcher
{
    public List<Ball> Instances { get; }
    public void Register(Ball instance);
}

public class BallInstancesWatcher : IBallsInstancesWatcher
{
    private List<Ball> _instances = new List<Ball>();

    public List<Ball> Instances
    {
        get => _instances;
    }
    
    public void Register(Ball instance)
    {
        _instances.Add(instance);
    }
}
    
