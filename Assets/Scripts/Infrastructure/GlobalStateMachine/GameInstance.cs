using KasherOriginal.Settings;
using KasherOriginal.AssetsAddressable;
using KasherOriginal.GlobalStateMachine;
using KasherOriginal.Factories.UIFactory;
using KasherOriginal.Factories.BallFactory;
using KasherOriginal.Factories.AbstractFactory;

public class GameInstance
{
    public GameInstance(IUIFactory uiFactory, IAssetsAddressableService assetsAddressableService, IAbstractFactory abstractFactory,
        GameSettings gameSettings, ICellsMatrixWatcher cellsMatrixWatcher, IShootableBallsContainer shootableBallsContainer,
        IGeneratedLevelCreator generatedLevelCreator, IBallsFactory ballsFactory)
    {
        StateMachine = new StateMachine<GameInstance>(this, 
            new BootstrapState(this),
            new SceneLoadingState(this, uiFactory), 
            new MainMenuState(this, uiFactory),
            new GameLoadingState(this, uiFactory),
            new SetUpGameplayState(this, assetsAddressableService, abstractFactory, gameSettings, generatedLevelCreator),
            new GameplayState(this, uiFactory, cellsMatrixWatcher, shootableBallsContainer),
            new LoseState(this, uiFactory, ballsFactory, abstractFactory),
            new WinState(this, uiFactory, ballsFactory, abstractFactory));
        
        StateMachine.SwitchState<BootstrapState>();
    }

    public readonly StateMachine<GameInstance> StateMachine;
}