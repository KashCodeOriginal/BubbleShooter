using TMPro;
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

    [SerializeField] private Image _nextBallColorSprite;
    [SerializeField] private TextMeshProUGUI _ballsCount;

    private IShootableBallsContainer _shootableBallsContainer;

    private void Start()
    {
        _shootableBallsContainer.NextBallColorChanged += ChangeNextBallInfo;
        
        ChangeNextBallInfo(_shootableBallsContainer.Balls[0].Color, _shootableBallsContainer.Balls.Count);
    }

    private void ChangeNextBallInfo(Color color, int amount)
    {
        _nextBallColorSprite.color = color;
        _ballsCount.text = amount.ToString();
    }

    private void OnDisable()
    {
        _shootableBallsContainer.NextBallColorChanged -= ChangeNextBallInfo;
    }
}
