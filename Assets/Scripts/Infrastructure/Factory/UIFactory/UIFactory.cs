using UnityEngine;
using System.Threading.Tasks;
using KasherOriginal.AssetsAddressable;
using Zenject;

public class UIFactory : IUIFactory
{
    public UIFactory(DiContainer container, IAssetsAddressableService assetsAddressableService)
    {
        _container = container;
        _assetsAddressableService = assetsAddressableService;
    }
    
    private readonly IAssetsAddressableService _assetsAddressableService;

    private readonly DiContainer _container;
    
    public GameObject LoadingGameScreen { get; private set; }
    public GameObject MainMenuScreen { get; private set; }

    public async Task<GameObject> CreateLoadingScreen()
    {
        var loadingScreenPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.LOADING_SCREEN);

        LoadingGameScreen = _container.InstantiatePrefab(loadingScreenPrefab);

        return LoadingGameScreen;
    }

    public void DestroyLoadingScreen()
    {
        Object.Destroy(LoadingGameScreen);
    }

    public async Task<GameObject> CreateMainMenuScreen()
    {
        var mainMenuPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.MAIN_MENU_SCREEN);

        MainMenuScreen = _container.InstantiatePrefab(mainMenuPrefab);

        return MainMenuScreen;
    }

    public void DestroyMainMenuScreen()
    {
        Object.Destroy(MainMenuScreen);
    }
}
