using UnityEngine;
using System.Collections.Generic;

namespace KasherOriginal.Factories.BallFactory
{
    public interface IBallsInfo
    {
        public IReadOnlyList<GameObject> Instances { get; }
    }
}

