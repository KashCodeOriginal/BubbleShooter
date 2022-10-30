using UnityEngine;

[CreateAssetMenu(fileName = "BallColorDecorator", menuName = "Decorators/BallSpriteBehavior/BallColorDecorator")]
public class BallColorDecorator : BallDecorator
{
    [SerializeField] private Color _color;
    [SerializeField] private BallTypeBehavior _ballType;

    public Color Color => _color;
    public BallTypeBehavior BallType => _ballType;
    
    public override void Decorate(ref BallStats ballStats)
    {
        ballStats.Color = _color;
        ballStats.BallTypeBehavior = _ballType;
    }
}
