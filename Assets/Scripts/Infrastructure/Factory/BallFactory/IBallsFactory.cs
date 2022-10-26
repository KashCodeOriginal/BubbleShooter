using System.Threading.Tasks;
using UnityEngine;

namespace KasherOriginal.Factories.BallFactory
{
    public interface IBallsFactory : IFactory, IBallsInfo
    {
        public Task<GameObject> CreateInstance(Vector3 position, params BallDecorator[] decorators);
    }
}