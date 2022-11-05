using UnityEngine;

public class LevelIndexContainer : MonoBehaviour
{
    [SerializeField] private int _levelIndex;

    public int LevelIndex => _levelIndex;
}
