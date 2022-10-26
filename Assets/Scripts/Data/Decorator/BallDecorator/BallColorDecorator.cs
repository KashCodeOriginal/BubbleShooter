using UnityEngine;

[CreateAssetMenu(fileName = "BallColorDecorator", menuName = "Decorators/Ball/BallColorDecorator")]
public class BallColorDecorator : BallDecorator
{
    [SerializeField] private Color _color;
    [SerializeField] private BallTypeBehavior _ballType;
    
    public override void Decorate(ref BallStats ballStats)
    {
        ballStats.Color = _color;
        ballStats.BallTypeBehavior = _ballType;
    }
}
