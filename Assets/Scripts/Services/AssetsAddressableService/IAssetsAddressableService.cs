using UnityEngine;
using System.Threading.Tasks;

namespace KasherOriginal.AssetsAddressable
{
    public interface IAssetsAddressableService
    {
        public Task<T> GetAsset<T>(string path) where T : Object;
    }
}

