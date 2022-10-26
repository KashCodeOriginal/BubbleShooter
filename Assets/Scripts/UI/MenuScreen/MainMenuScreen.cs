using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuScreen : MonoBehaviour
{
    public event UnityAction OnStartGameButtonClicked;

    [SerializeField] private Button _gameStartButton;

    public Button GameStartButton => _gameStartButton;

    private void Start()
    {
        _gameStartButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    private void PlayButtonClicked()
    {
        OnStartGameButtonClicked?.Invoke();
    }
}
