using KasherOriginal.Factories.AbstractFactory;
using UnityEngine;
using KasherOriginal.Factories.UIFactory;
using KasherOriginal.Factories.BallFactory;

namespace KasherOriginal.GlobalStateMachine
{
    public class LoseState : State<GameInstance>
    {
        public LoseState(GameInstance context, IUIFactory uiFactory, IBallsFactory ballsFactory, IAbstractFactory abstractFactory) : base(context)
        {
            _uiFactory = uiFactory;
            _ballsFactory = ballsFactory;
            _abstractFactory = abstractFactory;
        }

        private readonly IUIFactory _uiFactory;

        private readonly IBallsFactory _ballsFactory;
        private readonly IAbstractFactory _abstractFactory;

        private GameObject _gameLoseScreenInstance;

        private GameLoseScreen _gameLoseScreen;

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
            _gameLoseScreenInstance = await _uiFactory.CreateGameLoseScreen();

            _gameLoseScreen = _gameLoseScreenInstance.GetComponent<GameLoseScreen>();

            _gameLoseScreen.ButtonToMenuClicked += ChangeStateToMenu;
        }
        
        private void HideUI()
        {
            _gameLoseScreen.ButtonToMenuClicked -= ChangeStateToMenu;
            
            _uiFactory.DestroyGameLoseScreen();
        }

        private void ChangeStateToMenu()
        {
            Context.StateMachine.SwitchState<MainMenuState>();
        }
    }
}
