using UnityEngine;
using KasherOriginal.Settings;
using KasherOriginal.AssetsAddressable;
using KasherOriginal.Factories.AbstractFactory;

namespace KasherOriginal.GlobalStateMachine
{
    public class SetUpGameplayState : StateOneParam<GameInstance, int>
    {
        public SetUpGameplayState(GameInstance context, IAssetsAddressableService assetsAddressableService,
            IAbstractFactory abstractFactory, GameSettings gameSettings, IGeneratedLevelCreator generatedLevelCreator) : base(context)
        {
            _assetsAddressableService = assetsAddressableService;
            _abstractFactory = abstractFactory;
            _gameSettings = gameSettings;
            _generatedLevelCreator = generatedLevelCreator;
        }

        private readonly IAssetsAddressableService _assetsAddressableService;
        private readonly IAbstractFactory _abstractFactory;
        private readonly GameSettings _gameSettings;
        private readonly IGeneratedLevelCreator _generatedLevelCreator;

        private LevelsContainer _levelsContainer;

        public override async void Enter(int index)
        {
            var mapPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.BASE_MAP);
            var cannonPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.CANNON);
            var cannonControlPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.CANNON_CONTROL);
            var levelBuilderPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.LEVEL_BUILDER);

            var cannonControlInstance = _abstractFactory.CreateInstance(cannonControlPrefab, Vector3.zero);
            var cannonInstance = _abstractFactory.CreateInstance(cannonPrefab, _gameSettings.CannonInstancePosition);
            var mapInstance = _abstractFactory.CreateInstance(mapPrefab, _gameSettings.BaseMapPosition);
            var levelBuilderInstance = _abstractFactory.CreateInstance(levelBuilderPrefab, Vector3.zero);

            SetUp(cannonInstance, cannonControlInstance, levelBuilderInstance, index);
            
            Context.StateMachine.SwitchState<GameplayState, BallSpawner, ILevelBuilder>(levelBuilderInstance.GetComponentInChildren<BallSpawner>(), levelBuilderInstance.GetComponent<ILevelBuilder>());
        }

        private void SetUp(GameObject cannon, GameObject cannonControl, GameObject levelBuilder, int levelIndex)
        {
            if (cannonControl.TryGetComponent(out IRotatable rotatable))
            {
                rotatable.Construct(cannon.transform, _gameSettings.CannonRotationSpeed);
            }

            if (cannonControl.TryGetComponent(out ObjectInput objectInput))
            {
                levelBuilder.GetComponentInChildren<BallInstanceCreater>().Construct(objectInput, cannon);
            }

            if (levelBuilder.GetComponentInChildren<BallSpawner>())
            {
                levelBuilder.GetComponentInChildren<BallSpawner>().Construct(cannon.transform.GetChild(0));
            }
            
            if (levelBuilder.TryGetComponent(out ILevelBuilder levelBuilderComponent))
            {
                if (levelIndex == -1)
                {
                    levelBuilderComponent.SetLevelGenerationWay(true, null);
                    return;
                }

                _levelsContainer = new LevelsContainer();

                var currentCell =
                    _generatedLevelCreator.CreateLevel(_levelsContainer.Levels[levelIndex]);

                levelBuilderComponent.SetLevelGenerationWay(false, currentCell);
            }
        }
    }
}
