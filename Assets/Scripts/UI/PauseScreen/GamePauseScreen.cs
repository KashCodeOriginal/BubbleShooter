using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GamePauseScreen : MonoBehaviour
{
    public event UnityAction BackToMenuButtonClicked;
    public event UnityAction ContinuePlayButtonClicked;
        
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private Button _continuePlayButton;

    private void Start()
    {
        _backToMenuButton.onClick.AddListener(BackToMenuButtonClicked);
        _continuePlayButton.onClick.AddListener(ContinuePlayButtonClicked);
    }
}
