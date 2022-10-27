using UnityEngine;

namespace KasherOriginal.Factories.AbstractFactory
{
    public interface IAbstractFactory : IAbstractFactoryInfo, IFactory
    {
        public GameObject CreateInstance(GameObject prefab, Vector3 spawnPoint);
    }
}

