using KasherOriginal.GlobalStateMachine;

public class GameInstance
{
    public GameInstance()
    {
        StateMachine = new StateMachine<GameInstance>(this, 
            new BootstrapState(this),
            new SceneLoadingState(this));
        
        StateMachine.SwitchState<BootstrapState>();
    }

    public readonly StateMachine<GameInstance> StateMachine;
}