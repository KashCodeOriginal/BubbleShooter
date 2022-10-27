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
    }
}

