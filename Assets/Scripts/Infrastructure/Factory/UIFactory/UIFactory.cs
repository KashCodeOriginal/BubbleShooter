using Zenject;
using UnityEngine;
using System.Threading.Tasks;
using KasherOriginal.AssetsAddressable;

namespace KasherOriginal.Factories.UIFactory
{
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
        public GameObject GameplayScreen { get; private set; }
        public GameObject GameLoseScreen { get; private set; }
        public GameObject GameWinScreen { get; private set; }

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

        public async Task<GameObject> CreateGameplayScreen()
        {
            var gameplayScreenPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.GAMEPLAY_SCREEN);

            GameplayScreen = _container.InstantiatePrefab(gameplayScreenPrefab);

            return GameplayScreen;
        }

        public void DestroyGameplayScreen()
        {
            Object.Destroy(GameplayScreen);
        }

        public async Task<GameObject> CreateGameLoseScreen()
        {
            var gameLoseScreenPrefab = await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.GAME_LOSE_SCREEN);

            GameLoseScreen = _container.InstantiatePrefab(gameLoseScreenPrefab);

            return GameLoseScreen;
        }

        public void DestroyGameLoseScreen()
        {
            Object.Destroy(GameLoseScreen);
        }

        public Task<GameObject> CreateGameWinScreen()
        {
            throw new System.NotImplementedException();
        }

        public void DestroyGameWinScreen()
        {
            throw new System.NotImplementedException();
        }
    }
}

