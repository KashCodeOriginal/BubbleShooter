using System;
using Zenject;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using KasherOriginal.AssetsAddressable;

namespace KasherOriginal.Factories.BallFactory
{
    public class BallsFactory : IBallsFactory
    {
        public BallsFactory(DiContainer container, IAssetsAddressableService assetsAddressableService)
        {
            _container = container;
            _assetsAddressableService = assetsAddressableService;
        }
    
        private DiContainer _container;
        private IAssetsAddressableService _assetsAddressableService;
        
        private List<GameObject> _instances = new List<GameObject>();
    
        public IReadOnlyList<GameObject> Instances
        {
            get => _instances;
        }

        public async Task<Ball> DecorateBall(params BallColorDecorator[] decorators)
        {
            Ball ball = new Ball();
            
            var ballConfig = await _assetsAddressableService.GetAsset<BallConfig>(AssetsAddressablesConstants.BASE_MOVING_BALL_CONFIG);

            BallStats ballStats = GetStatsFromBall(ballConfig);
            
            DecorateStats(ref ballStats, decorators);

            ball.Modify(ballStats.Color, ballStats.BallTypeBehavior);

            return ball;
        }

        public async Task<GameObject> CreateMovableInstance(Vector2 position, Ball ball)
        {
            var ballConfig = await _assetsAddressableService.GetAsset<BallConfig>(AssetsAddressablesConstants.BASE_MOVING_BALL_CONFIG);

            var instanceWithStats = CreateInstanceWithStats(ballConfig, position, ball);

            return instanceWithStats;
        }

        public async Task<GameObject> CreateStaticInstance(Vector2 position, BallColorDecorator[] decorators)
        {
            var ballConfig = await _assetsAddressableService.GetAsset<BallConfig>(AssetsAddressablesConstants.BASE_STATIC_BALL_CONFIG);

            var instanceWithStats = CreateInstanceWithStats(ballConfig, position, decorators);

            return instanceWithStats;
        }
        
        public void DestroyInstance(GameObject instance)
        {
            if (instance == null)
            {
                Debug.LogError("There is no instance to destroy");
            }
            if (!_instances.Contains(instance))
            {
                Debug.LogError("This instance wasn't created by balls factory");
            }
            
            Object.Destroy(instance);
            _instances.Remove(instance);
        }
    
        public void DestroyAllInstances()
        {
            for (int i = 0; i < _instances.Count; i++)
            {
                Object.Destroy(_instances[i]);
            }
            
            _instances.Clear();
        }

        public void DestroyAllInstances<T>(List<T> list) where T : Object
        {
            for (int i = 0; i < list.Count; i++)
            {
                Object.Destroy(list[i]);
            }
        
            list.Clear();
        }


        private GameObject CreateInstanceWithStats(BallConfig ballConfig, Vector2 position, params BallColorDecorator[] decorators)
        {
            BallStats ballStats = GetStatsFromBall(ballConfig);

            GameObject ballInstance = SpawnGameObject(ballConfig);
            
            DecorateStats(ref ballStats, decorators);

            SetUp(ballInstance, position, ballStats);

            _instances.Add(ballInstance);
    
            ToScene(ballInstance);
    
            return ballInstance;
        }
        
        private GameObject CreateInstanceWithStats(BallConfig ballConfig, Vector2 position, Ball ball)
        {
            GameObject ballInstance = SpawnGameObject(ballConfig);

            SetUp(ballInstance, position, ball);
            
            _instances.Add(ballInstance);
    
            ToScene(ballInstance);
    
            return ballInstance;
        }
    
        private BallStats GetStatsFromBall(BallConfig ballConfig)
        {
            return new BallStats(
                ballConfig.Color,
                ballConfig.BallType);
        }
    
        private GameObject SpawnGameObject(BallConfig ballConfig)
        {
            if (ballConfig.Prefab == null)
            {
                throw new NullReferenceException($"There is no prefab on {ballConfig}");
            }
            
            GameObject ballInstance = _container.InstantiatePrefab(ballConfig.Prefab);
    
            return ballInstance;
        }
        
        private void DecorateStats(ref BallStats ballStats, params BallColorDecorator[] decorators)
        {
            foreach (var decorator in decorators)
            {
                decorator.Decorate(ref ballStats);
            }
        }
    
        private void SetUp(GameObject ballInstance, Vector3 position, BallStats ballStats)
        {
            ballInstance.transform.position = position;
    
            if (ballInstance.TryGetComponent(out BallSpriteBehavior ball))
            {
                ball.Modify(ballStats.Color, ballStats.BallTypeBehavior);
            }
        }
        
        private void SetUp(GameObject ballInstance, Vector3 position, Ball ball)
        {
            ballInstance.transform.position = position;
    
            if (ballInstance.TryGetComponent(out BallSpriteBehavior ballSprite))
            {
                ballSprite.Modify(ball.Color, ball.BallType);
            }
        }
    
        private void ToScene(GameObject instance)
        {
            SceneManager.MoveGameObjectToScene(instance, SceneManager.GetActiveScene());
        }
    }
}