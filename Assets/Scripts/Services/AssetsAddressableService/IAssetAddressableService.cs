using UnityEngine;
using System.Threading.Tasks;

public interface IAssetAddressableService
{
    public Task<T> GetAsset<T>(string path) where T : Object;
}
