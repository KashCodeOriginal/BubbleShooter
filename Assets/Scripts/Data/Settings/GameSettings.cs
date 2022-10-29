using UnityEngine;

namespace KasherOriginal.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    public class GameSettings : BaseSettings
    {
        [SerializeField] private Vector3 _cannonInstancePosition;
        
        [SerializeField] private Vector3 _baseMapPosition;
        
        [SerializeField] private float _cannonRotationSpeed;

        [SerializeField] private int _ballMovementSpeed;

        [SerializeField] private int _maxBallWallsCollides;

        [SerializeField] private int _targetFPS;

        [SerializeField] private int _minBallsAmount;
        [SerializeField] private int _maxBallsAmount;

        public Vector3 CannonInstancePosition => _cannonInstancePosition;
        public Vector3 BaseMapPosition => _baseMapPosition;
        public float CannonRotationSpeed => _cannonRotationSpeed;
        public int BallMovementSpeed => _ballMovementSpeed;
        public int MaxBallWallsCollider => _maxBallWallsCollides;
        
        public int MinBallsAmount => _minBallsAmount;
        public int MaxBallsAmount => _maxBallsAmount;

        private void OnEnable()
        {
            Application.targetFrameRate = _targetFPS;
        }
    }
}