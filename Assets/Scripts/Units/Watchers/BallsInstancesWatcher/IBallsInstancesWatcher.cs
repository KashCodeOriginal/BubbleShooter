using System.Collections.Generic;

public interface IBallsInstancesWatcher
{
    public List<BallSpriteBehavior> Instances { get; }
    public void Register(BallSpriteBehavior instance);
}

public class BallInstancesWatcher : IBallsInstancesWatcher
{
    private List<BallSpriteBehavior> _instances = new List<BallSpriteBehavior>();

    public List<BallSpriteBehavior> Instances
    {
        get => _instances;
    }
    
    public void Register(BallSpriteBehavior instance)
    {
        _instances.Add(instance);
    }
}
    
