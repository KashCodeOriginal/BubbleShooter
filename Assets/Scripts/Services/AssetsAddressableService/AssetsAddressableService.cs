using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace KasherOriginal.AssetsAddressable
{
    public class AssetsAddressableService : IAssetsAddressableService
    {
        public async Task<T> GetAsset<T>(string path) where T : Object
        {
            var asyncOperationHandle = Addressables.LoadAssetAsync<T>(path);

            await asyncOperationHandle.Task;

            return asyncOperationHandle.Result;
        }
    }
}