using Zenject;
using KasherOriginal.GlobalStateMachine;

public class StatesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindBootstrapState();
    }

    private void BindBootstrapState()
    {
        Container.Bind<IInitializable>().To<BootstrapState>().AsSingle().NonLazy();
    }
}
