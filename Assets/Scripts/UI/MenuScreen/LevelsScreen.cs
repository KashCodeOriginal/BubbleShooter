using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class LevelsScreen : MonoBehaviour
{
    public event UnityAction<int> GeneratedLevelButtonClicked;

    [SerializeField] private List<Button> _levelButtons;

    private void Start()
    {
        foreach (var button in _levelButtons)
        {
            var index = button.GetComponent<LevelIndexContainer>().LevelIndex;
            
            button.onClick.AddListener(delegate { GeneratedLevelButtonClicked?.Invoke(index); });
        }
    }
}
