using KasherOriginal.Factories.UIFactory;
using KasherOriginal.Factories.BallFactory;
using KasherOriginal.Factories.AbstractFactory;

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


        public override void Enter()
        {
            ShowUI();
        }

        private void ShowUI()
        {
            _uiFactory.CreateGameLoseScreen();
        }
        
        private void HideUI()
        {
            _uiFactory.DestroyGameLoseScreen();
        }
    }
}
