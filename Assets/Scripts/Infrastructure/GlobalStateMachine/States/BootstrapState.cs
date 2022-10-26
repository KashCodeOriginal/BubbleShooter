using UnityEngine;
using Zenject;

namespace KasherOriginal.GlobalStateMachine
{
    public class BootstrapState : State<GameInstance>, IInitializable
    {
        public BootstrapState(GameInstance context) : base(context) { }

        public void Initialize()
        {
            Context.StateMachine.SwitchState<SceneLoadingState>();
        }
    }
}