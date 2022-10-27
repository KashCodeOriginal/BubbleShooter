using UnityEngine.AddressableAssets;
using KasherOriginal.AssetsAddressable;
using KasherOriginal.Factories.UIFactory;

namespace KasherOriginal.GlobalStateMachine
{
    public class GameLoadingState : State<GameInstance>
    {
        public GameLoadingState(GameInstance context, IUIFactory uiFactory) : base(context)
        {
            _uiFactory = uiFactory;
        }

        private readonly IUIFactory _uiFactory;

        public override async void Enter()
        {
            ShowUI();
            
            var asyncOperationHandle = Addressables.LoadSceneAsync(AssetsAddressablesConstants.GAMEPLAY_LEVEL_NAME);
            await asyncOperationHandle.Task;
            
            Context.StateMachine.SwitchState<SetUpGameplayState>();
        }

        public override void Exit()
        {
            HideUI();   
        }

        private void ShowUI()
        {
            _uiFactory.CreateLoadingScreen();
        }

        private void HideUI()
        {
            _uiFactory.DestroyLoadingScreen();
        }
    }
}