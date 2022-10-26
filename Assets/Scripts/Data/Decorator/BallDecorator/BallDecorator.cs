using UnityEngine;

public abstract class BallDecorator : ScriptableObject
{
    public abstract void Decorate(ref BallStats ballStats);
}
