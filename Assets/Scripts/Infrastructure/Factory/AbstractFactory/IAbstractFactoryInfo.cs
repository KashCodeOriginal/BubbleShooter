using UnityEngine;
using System.Collections.Generic;

namespace KasherOriginal.Factories.AbstractFactory
{
    public interface IAbstractFactoryInfo
    {
        public List<GameObject> Instances { get; }
    }
}
