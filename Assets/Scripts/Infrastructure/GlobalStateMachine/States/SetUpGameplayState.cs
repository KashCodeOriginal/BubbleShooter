using UnityEngine;
using KasherOriginal.Settings;
using KasherOriginal.AssetsAddressable;
using KasherOriginal.Factories.AbstractFactory;

namespace KasherOriginal.GlobalStateMachine
{
    public class SetUpGameplayState : StateOneParam<GameInstance, bool>
    {
        public SetUpGameplayState(GameInstance context, IAssetsAddressableService assetsAddressableService,
            IAbstractFactory abstractFactory, GameSettings gameSettings) : base(context)
        {
            _assetsAddressableService = assetsAddressableService;
            _abstractFactory = abstractFactory;
            _gameSettings = gameSettings;
        }

        private readonly IAssetsAddressableService _assetsAddressableService;
        private readonly IAbstractFactory _abstractFactory;
        private readonly GameSettings _gameSettings;

        public override async void Enter(bool isLevelRandom)
        {
            var mapPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.BASE_MAP);
            var cannonPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.CANNON);
            var cannonControlPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.CANNON_CONTROL);
            var levelBuilderPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.LEVEL_BUILDER);

            var cannonControlInstance = _abstractFactory.CreateInstance(cannonControlPrefab, Vector3.zero);
            var cannonInstance = _abstractFactory.CreateInstance(cannonPrefab, _gameSettings.CannonInstancePosition);
            var mapInstance = _abstractFactory.CreateInstance(mapPrefab, _gameSettings.BaseMapPosition);
            var levelBuilderInstance = _abstractFactory.CreateInstance(levelBuilderPrefab, Vector3.zero);
            

            SetUp(cannonInstance, cannonControlInstance, levelBuilderInstance, isLevelRandom);
            
            Context.StateMachine.SwitchState<GameplayState>();
        }

        private void SetUp(GameObject cannon, GameObject cannonControl, GameObject levelBuilder, bool isLevelRandom)
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
            
            if (levelBuilder.TryGetComponent(out LevelBuilder levelBuilderComponent))
            {
                if (isLevelRandom)
                {
                    levelBuilderComponent.SetLevelGenerationWay(true, null);
                    return;
                }
                
                levelBuilderComponent.SetLevelGenerationWay(false, new Cell[CellsMatrixWatcher.ROWS_COUNT,CellsMatrixWatcher.COLUMNS_COUNT]);
            }
        }
    }
}
