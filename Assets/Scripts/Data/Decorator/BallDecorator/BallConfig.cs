using UnityEngine;

[CreateAssetMenu(fileName = "BallDecoratorConfig", menuName = "Decorators/BaseConfigs/BallDecoratorConfig")]
public class BallConfig : ScriptableObject
{
    [SerializeField] private Color _color;
    [SerializeField] private BallTypeBehavior _ballType;
    [SerializeField] private GameObject _prefab;

    public Color Color => _color;
    public BallTypeBehavior BallType => _ballType;
    public GameObject Prefab => _prefab;
}
