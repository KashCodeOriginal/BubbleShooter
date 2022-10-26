using Zenject;
using KasherOriginal.AssetsAddressable;

public class ServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    { 
        BindAssetsAddressable();
    }

    private void BindAssetsAddressable()
    {
        Container.BindInterfacesTo<AssetsAddressableService>().AsSingle();
    }
}
