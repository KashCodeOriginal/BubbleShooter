using KasherOriginal.AssetsAddressable;
using KasherOriginal.GlobalStateMachine;
using KasherOriginal.Factories.UIFactory;
using KasherOriginal.Factories.AbstractFactory;
using KasherOriginal.Settings;

public class GameInstance
{
    public GameInstance(IUIFactory uiFactory, IAssetsAddressableService assetsAddressableService, IAbstractFactory abstractFactory,
        GameSettings gameSettings)
    {
        StateMachine = new StateMachine<GameInstance>(this, 
            new BootstrapState(this),
            new SceneLoadingState(this, uiFactory), 
            new MainMenuState(this, uiFactory),
            new GameLoadingState(this, uiFactory),
            new SetUpGameplayState(this, assetsAddressableService, abstractFactory, gameSettings),
            new GameplayState(this));
        
        StateMachine.SwitchState<BootstrapState>();
    }

    public readonly StateMachine<GameInstance> StateMachine;
}