using KasherOriginal.Factories.UIFactory;

namespace KasherOriginal.GlobalStateMachine
{
    public class GameplayState : StateOneParam<GameInstance, BallSpawner>
    {
        public GameplayState(GameInstance context, IUIFactory uiFactory, ICellsMatrixWatcher cellsMatrixWatcher,
            IShootableBallsContainer shootableBallsContainer) :
            base(context)
        {
            _uiFactory = uiFactory;
            _cellsMatrixWatcher = cellsMatrixWatcher;
            _shootableBallsContainer = shootableBallsContainer;
        }

        private readonly IUIFactory _uiFactory;
        private readonly ICellsMatrixWatcher _cellsMatrixWatcher;
        private readonly IShootableBallsContainer _shootableBallsContainer;

        private BallSpawner _ballSpawner;
        

        public override void Enter(BallSpawner ballSpawner)
        {
            _ballSpawner = ballSpawner;
            
            _cellsMatrixWatcher.BallOutOfBorder += ChangeToLoseState;
            _cellsMatrixWatcher.PlayerWonGame += ChangeToWinState;
            _ballSpawner.BallsAmountIsZero += ChangeToLoseState;

            ShowUI();
        }

        public override void Exit()
        {
            _cellsMatrixWatcher.BallOutOfBorder -= ChangeToLoseState;
            _cellsMatrixWatcher.PlayerWonGame -= ChangeToWinState;
            _ballSpawner.BallsAmountIsZero -= ChangeToLoseState;
            
            _shootableBallsContainer.DeleteAllBalls();

            HideUI();
        }

        private void ChangeToLoseState()
        {
            Context.StateMachine.SwitchState<LoseState>();
        }
        
        private void ChangeToWinState()
        {
            Context.StateMachine.SwitchState<WinState>();
        }

        private void ShowUI()
        {
            _uiFactory.CreateGameplayScreen();
        }
        
        private void HideUI()
        {
            _uiFactory.DestroyGameplayScreen();
        }
    }
}