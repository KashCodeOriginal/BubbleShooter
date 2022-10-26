using UnityEngine;

namespace KasherOriginal.Factories
{
    public interface IFactory
    {
        public void DestroyInstance(GameObject instance);
        public void DestroyAllInstances();
    }
}