using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameLoseScreen : MonoBehaviour
{
    public event UnityAction ButtonToMenuClicked;
    
    [SerializeField] private Button _toMenuButton;

    private void Start()
    {
        _toMenuButton.onClick.AddListener(ButtonToMenuClicked);
    }
}
