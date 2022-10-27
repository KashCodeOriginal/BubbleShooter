using UnityEngine;

public class BallShooter : MonoBehaviour
{
    private BallSpawner _ballSpawner;

    private void Start()
    {
        _ballSpawner = FindObjectOfType<BallSpawner>();
    }
}
