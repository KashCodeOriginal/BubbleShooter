using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private BallTypeBehavior _ballType;

    public BallTypeBehavior BallType => _ballType;
    
    public void Modify(Color color, BallTypeBehavior ballType)
    {
        _spriteRenderer.color = color;
        _ballType = ballType;
    }
}