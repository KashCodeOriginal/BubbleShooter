using KasherOriginal.Factories.UIFactory;

namespace KasherOriginal.GlobalStateMachine
{
    public class GameplayState : State<GameInstance>
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
        

        public override void Enter()
        {
            _cellsMatrixWatcher.BallOutOfBorder += ChangeToLoseState;

            ShowUI();
        }

        public override void Exit()
        {
            _cellsMatrixWatcher.BallOutOfBorder -= ChangeToLoseState;
            
            _shootableBallsContainer.DeleteAllBalls();

            HideUI();
        }

        private void ChangeToLoseState()
        {
            Context.StateMachine.SwitchState<LoseState>();
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