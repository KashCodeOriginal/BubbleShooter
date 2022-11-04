using KasherOriginal.Factories.UIFactory;
using KasherOriginal.Factories.BallFactory;
using KasherOriginal.Factories.AbstractFactory;
using UnityEngine;

namespace KasherOriginal.GlobalStateMachine
{
    public class WinState : State<GameInstance>
    {
        public WinState(GameInstance context, IUIFactory uiFactory, IBallsFactory ballsFactory, IAbstractFactory abstractFactory) : base(context)
        {
            _uiFactory = uiFactory;
            _ballsFactory = ballsFactory;
            _abstractFactory = abstractFactory;
        }

        private readonly IUIFactory _uiFactory;
        private readonly IBallsFactory _ballsFactory;
        private readonly IAbstractFactory _abstractFactory;

        private GameObject _gameWinScreenInstance;
        private GameWinScreen _gameWinScreen;


        public override void Enter()
        {
            ShowUI();
        }

        public override void Exit()
        {
            HideUI();
            
            _ballsFactory.DestroyAllInstances();
            
            _abstractFactory.DestroyAllInstances();
        }

        private async void ShowUI()
        {
            _gameWinScreenInstance = await _uiFactory.CreateGameLoseScreen();

            _gameWinScreen = _gameWinScreenInstance.GetComponent<GameWinScreen>();

            _gameWinScreen.ButtonToMenuClicked += ChangeStateToMenu;
        }
        
        private void HideUI()
        {
            _gameWinScreen.ButtonToMenuClicked -= ChangeStateToMenu;
            
            _uiFactory.DestroyGameWinScreen();
        }

        private void ChangeStateToMenu()
        {
            Context.StateMachine.SwitchState<MainMenuState>();
        }
    }
}
