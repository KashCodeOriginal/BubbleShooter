using System;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScreen : MonoBehaviour
{
    [Inject]
    public void Construct(IShootableBallsContainer shootableBallsContainer)
    {
        _shootableBallsContainer = shootableBallsContainer;
    }

    private void Start()
    {
        _shootableBallsContainer.NextBallColorChanged += ChangeNextBallColor;
        
        ChangeNextBallColor(_shootableBallsContainer.Balls[0].Color);
    }

    [SerializeField] private Image _nextBallColorSprite;

    private IShootableBallsContainer _shootableBallsContainer;

    private void ChangeNextBallColor(Color color)
    {
        _nextBallColorSprite.color = color;
    }

    private void OnDisable()
    {
        _shootableBallsContainer.NextBallColorChanged -= ChangeNextBallColor;
    }
}
