using UnityEngine;
using System.Threading.Tasks;
using KasherOriginal.AssetsAddressable;

public class UIFactory : IUIFactory
{
    public UIFactory(IAssetsAddressableService assetsAddressableService)
    {
        _assetsAddressableService = assetsAddressableService;
    }
    
    private readonly IAssetsAddressableService _assetsAddressableService;
    
    public GameObject LoadingGameScreen { get; }

    public Task<GameObject> CreateLoadingScreen()
    {
        //_assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.LOADING_LEVEL_NAME)
        return null;
    }

    public void DestroyLoadingScreen()
    {
        throw new System.NotImplementedException();
    }
}
