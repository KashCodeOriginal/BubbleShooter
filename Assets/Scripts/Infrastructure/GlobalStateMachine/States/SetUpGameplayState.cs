using UnityEngine;
using KasherOriginal.Settings;
using KasherOriginal.AssetsAddressable;
using KasherOriginal.Factories.AbstractFactory;

namespace KasherOriginal.GlobalStateMachine
{
    public class SetUpGameplayState : State<GameInstance>
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

        public override async void Enter()
        {
            var mapPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.BASE_MAP);
            var cannonPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.CANNON);
            var cannonControl = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.CANNON_CONTROL);

            var cannonControlInstance = _abstractFactory.CreateInstance(cannonControl, Vector3.zero);
            var cannonInstance = _abstractFactory.CreateInstance(cannonPrefab, _gameSettings.CannonInstancePosition);
            var mapInstance = _abstractFactory.CreateInstance(mapPrefab, _gameSettings.BaseMapPosition);

            SetUp(cannonInstance, cannonControlInstance);
            
            Context.StateMachine.SwitchState<GameplayState>();
        }

        private void SetUp(GameObject cannon, GameObject cannonControl)
        {
            if (cannonControl.TryGetComponent(out IRotatable rotatable))
            {
                rotatable.Construct(cannon.transform, _gameSettings.CannonRotationSpeed);
            }

            if (cannonControl.TryGetComponent(out ObjectInput objectInput))
            {
                cannon.GetComponentInChildren<BallCreater>().Construct(objectInput, cannon);
            }
        }
    }
}
