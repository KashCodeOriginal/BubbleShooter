using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuScreen : MonoBehaviour
{
    public event UnityAction RandomLevelButtonClicked;

    [SerializeField] private Button _randomLevelButton;

    [SerializeField] private LevelsScreen _levelsScreen;

    public LevelsScreen LevelsScreen => _levelsScreen;

    private void Start()
    {
        _randomLevelButton.onClick.AddListener(RandomLevelButtonClicked);
    }
}
