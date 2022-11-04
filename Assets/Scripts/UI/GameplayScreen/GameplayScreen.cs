using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameplayScreen : MonoBehaviour
{
    [Inject]
    public void Construct(IShootableBallsContainer shootableBallsContainer)
    {
        _shootableBallsContainer = shootableBallsContainer;
    }

    public event UnityAction PauseButtonWasClicked;

    [SerializeField] private Image _nextBallColorSprite;
    [SerializeField] private TextMeshProUGUI _ballsCount;
    [SerializeField] private Button _pauseButton;

    private IShootableBallsContainer _shootableBallsContainer;

    private void Start()
    {
        _shootableBallsContainer.NextBallColorChanged += ChangeNextBallInfo;
        
        _pauseButton.onClick.AddListener(PauseButtonWasClicked);
        
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
