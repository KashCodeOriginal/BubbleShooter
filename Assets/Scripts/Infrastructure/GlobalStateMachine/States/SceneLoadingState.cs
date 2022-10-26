using UnityEngine.AddressableAssets;
using KasherOriginal.AssetsAddressable;

namespace KasherOriginal.GlobalStateMachine
{
    public class SceneLoadingState : State<GameInstance>
    {
        public SceneLoadingState(GameInstance context, IUIFactory uiFactory) : base(context)
        {
            _uiFactory = uiFactory;
        }

        private readonly IUIFactory _uiFactory;
        
        public override async void Enter()
        {
            ShowUI();
            
            var asyncOperationHandle = Addressables.LoadSceneAsync(AssetsAddressablesConstants.MAIN_MENU_LEVEL_NAME);
            await asyncOperationHandle.Task;

            OnSceneLoadingComplete();
        }

        public override void Exit()
        {
            HideUI();
        }

        private void OnSceneLoadingComplete()
        {
            Context.StateMachine.SwitchState<MainMenuState>();
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