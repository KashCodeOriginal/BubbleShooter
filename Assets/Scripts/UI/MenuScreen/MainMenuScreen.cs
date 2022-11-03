using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuScreen : MonoBehaviour
{
    public event UnityAction RandomLevelButtonClicked;
    public event UnityAction GeneratedLevelButtonClicked;
    
    [SerializeField] private Button _randomLevelButton;
    
    [SerializeField] private Button _generatedLevelButton;

    private void Start()
    {
        _randomLevelButton.onClick.AddListener(RandomLevelButtonClicked);
        _generatedLevelButton.onClick.AddListener(GeneratedLevelButtonClicked);
    }
}
