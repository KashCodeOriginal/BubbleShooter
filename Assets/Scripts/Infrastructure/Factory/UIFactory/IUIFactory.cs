using System.Threading.Tasks;
using UnityEngine;

public interface IUIFactory : IUIInfo
{
    public Task<GameObject> CreateLoadingScreen();
    public void DestroyLoadingScreen();
    public Task<GameObject> CreateMainMenuScreen();
    public void DestroyMainMenuScreen();
}
