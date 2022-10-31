using System.Threading.Tasks;
using UnityEngine;

namespace KasherOriginal.Factories.BallFactory
{
    public interface IBallsFactory : IFactory, IBallsInfo
    {
        public Task<Ball> DecorateBall(params BallColorDecorator[] decorators);
        public Task<GameObject> CreateMovableInstance(Vector2 position, Ball ball);
        public Task<GameObject> CreateStaticInstance(Vector2 positions, params BallColorDecorator[] decorator);
    }
}