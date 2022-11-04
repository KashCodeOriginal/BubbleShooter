using UnityEngine;
using System.Threading.Tasks;

namespace KasherOriginal.Factories.UIFactory
{
    public interface IUIFactory : IUIInfo
    {
        public Task<GameObject> CreateLoadingScreen();
        public void DestroyLoadingScreen();
        public Task<GameObject> CreateMainMenuScreen();
        public void DestroyMainMenuScreen();
        public Task<GameObject> CreateGameplayScreen();
        public void DestroyGameplayScreen();
        public Task<GameObject> CreateGameLoseScreen();
        public void DestroyGameLoseScreen();
        public Task<GameObject> CreateGameWinScreen();
        public void DestroyGameWinScreen();
        public Task<GameObject> CreateGamePauseScreen();
        public void DestroyGamePauseScreen();
    }
}

