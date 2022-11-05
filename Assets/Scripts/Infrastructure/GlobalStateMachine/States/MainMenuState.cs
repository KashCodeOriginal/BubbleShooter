using KasherOriginal.Factories.UIFactory;

namespace KasherOriginal.GlobalStateMachine
{
    public class MainMenuState : State<GameInstance>
    {
        public MainMenuState(GameInstance context, IUIFactory uiFactory) : base(context)
        {
            _uiFactory = uiFactory;
        }

        private readonly IUIFactory _uiFactory;

        private MainMenuScreen _mainMenuScreen;
        

        public override void Enter()
        {
            ShowUI();
        }

        public override void Exit()
        {
            HideUI();
        }

        private async void ShowUI()
        {
            var gameStartScreen = await _uiFactory.CreateMainMenuScreen();

            _mainMenuScreen = gameStartScreen.GetComponent<MainMenuScreen>();
            
            _mainMenuScreen.RandomLevelButtonClicked += RandomLevelSelected;
            _mainMenuScreen.LevelsScreen.GeneratedLevelButtonClicked += GeneratedLevelSelected;
        }

        private void HideUI()
        {
            _uiFactory.DestroyMainMenuScreen();
        }
        
        private void RandomLevelSelected()
        {
            int index = -1;
            
            Context.StateMachine.SwitchState<GameLoadingState, int>(index);
        }
        
        private void GeneratedLevelSelected(int index)
        {
            Context.StateMachine.SwitchState<GameLoadingState, int>(index);
        }
    }

}