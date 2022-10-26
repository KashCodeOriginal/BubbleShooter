using UnityEngine.AddressableAssets;
using KasherOriginal.AssetsAddressable;

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
            
            var asyncOperationHandle = Addressables.LoadSceneAsync(AssetsAddressablesConstants.FIRST_GAME_LEVEL_NAME);
            await asyncOperationHandle.Task;
            
            Context.StateMachine.SwitchState<GameplayState>();
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