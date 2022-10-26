using Zenject;
using KasherOriginal.AssetsAddressable;

public class ServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    { 
        BindAssetsAddressable();
        BindUIFabric();
    }

    private void BindAssetsAddressable()
    {
        Container.BindInterfacesTo<AssetsAddressableService>().AsSingle();
    }

    private void BindUIFabric()
    {
        Container.BindInterfacesTo<UIFactory>().AsSingle();
    }
}
