using System.Threading.Tasks;
using UnityEngine;

namespace KasherOriginal.Factories.BallFactory
{
    public interface IBallsFactory : IFactory, IBallsInfo
    {
        public Task<GameObject> CreateDecoratableInstance(Vector3 position, bool isInstanceMovable, params BallDecorator[] decorators);
    }
}