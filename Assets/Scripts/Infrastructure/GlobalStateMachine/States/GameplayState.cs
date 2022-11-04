using KasherOriginal.Factories.UIFactory;
using UnityEngine;

namespace KasherOriginal.GlobalStateMachine
{
    public class GameplayState : StateTwoParam<GameInstance, BallSpawner, ILevelBuilder>
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

        private GameObject _gameplayScreenInstance;
        private GameplayScreen _gameplayScreen;
        
        private GameObject _gamePauseScreenInstance;
        private GamePauseScreen _gamePauseScreen;

        private BallSpawner _ballSpawner;
        private ILevelBuilder _levelBuilder;
        

        public override void Enter(BallSpawner ballSpawner, ILevelBuilder levelBuilder)
        {
            _ballSpawner = ballSpawner;
            _levelBuilder = levelBuilder;
            
            _cellsMatrixWatcher.BallOutOfBorder += ChangeToLoseState;
            _levelBuilder.PlayerWonGame += ChangeToWinState;
            _ballSpawner.BallsAmountIsZero += ChangeToLoseState;
            ShowUI();
        }

        public override void Exit()
        {
            _cellsMatrixWatcher.BallOutOfBorder -= ChangeToLoseState;
            _levelBuilder.PlayerWonGame -= ChangeToWinState;
            _ballSpawner.BallsAmountIsZero -= ChangeToLoseState;
            _gamePauseScreen.BackToMenuButtonClicked -= ChangeToLoseState;
            
            _shootableBallsContainer.DeleteAllBalls();
            _uiFactory.DestroyGamePauseScreen();

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

        private async void ShowUI()
        {
            _gameplayScreenInstance = await _uiFactory.CreateGameplayScreen();

            _gameplayScreen = _gameplayScreenInstance.GetComponent<GameplayScreen>();

            _gameplayScreen.PauseButtonWasClicked += CreatePauseScreen;
        }
        
        private void HideUI()
        {
            _gameplayScreen.PauseButtonWasClicked -= CreatePauseScreen;
            
            _uiFactory.DestroyGameplayScreen();
        }

        private async void CreatePauseScreen()
        {
            _gamePauseScreenInstance = await _uiFactory.CreateGamePauseScreen();

            _gamePauseScreen = _gamePauseScreenInstance.GetComponent<GamePauseScreen>();

            _gamePauseScreen.ContinuePlayButtonClicked += DestroyPauseScreen;

            _gamePauseScreen.BackToMenuButtonClicked += ChangeToLoseState;
        }

        private void DestroyPauseScreen()
        {
            _uiFactory.DestroyGamePauseScreen();
            _gamePauseScreen.ContinuePlayButtonClicked -= DestroyPauseScreen;
        }
    }
}