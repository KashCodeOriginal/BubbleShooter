using UnityEngine.AddressableAssets;

namespace KasherOriginal.GlobalStateMachine
{
    public class SceneLoadingState : State<GameInstance>
    {
        public SceneLoadingState(GameInstance context) : base(context)
        {
            
        }

        public override async void Enter()
        {
            var asyncOperationHandle = Addressables.LoadSceneAsync(AssetsAddressablesConstants.MAIN_MENU_LEVEL_NAME);
            await asyncOperationHandle.Task;

            OnSceneLoadingComplete();
        }

        private void OnSceneLoadingComplete()
        {
            Context.StateMachine.SwitchState<MainMenuState>();
        }
    }
}