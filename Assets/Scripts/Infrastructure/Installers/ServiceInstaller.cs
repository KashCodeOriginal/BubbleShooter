using Zenject;
using KasherOriginal.AssetsAddressable;
using KasherOriginal.Factories.UIFactory;
using KasherOriginal.Factories.BallFactory;

public class ServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindUIFactory();
        BindBallsFactory();
        
        BindAssetsAddressable();
    }

    private void BindAssetsAddressable()
    {
        Container.BindInterfacesTo<AssetsAddressableService>().AsSingle();
    }

    private void BindUIFactory()
    {
        Container.BindInterfacesTo<UIFactory>().AsSingle();
    }

    private void BindBallsFactory()
    {
        Container.BindInterfacesTo<BallsFactory>().AsSingle();
    }

    private void BindAbstractFactory()
    {
        Container.BindInterfacesTo<AbstractFactory>().AsSingle();
    }
}
