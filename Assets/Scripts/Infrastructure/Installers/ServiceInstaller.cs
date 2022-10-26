using Zenject;
using KasherOriginal.AssetsAddressable;
using KasherOriginal.Factories.BallFactory;

public class ServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindUIFabric();
        BindBallsFabric();
        BindAssetsAddressable();
    }

    private void BindAssetsAddressable()
    {
        Container.BindInterfacesTo<AssetsAddressableService>().AsSingle();
    }

    private void BindUIFabric()
    {
        Container.BindInterfacesTo<UIFactory>().AsSingle();
    }

    private void BindBallsFabric()
    {
        Container.BindInterfacesTo<BallsFactory>().AsSingle();
    }
}
