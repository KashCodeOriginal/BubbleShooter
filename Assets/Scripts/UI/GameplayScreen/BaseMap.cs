using UnityEngine;

public class BaseMap : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backgroundImage;

    [SerializeField] private Sprite[] _backgrounds;

    private void Start()
    {
        _backgroundImage.sprite = _backgrounds[Random.Range(0, _backgrounds.Length)];
    }
}
