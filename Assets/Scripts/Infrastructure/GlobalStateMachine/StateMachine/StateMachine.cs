﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace KasherOriginal.GlobalStateMachine
{
    public class StateMachine<TContext>
    {
        private readonly Dictionary<Type, BaseState<TContext>> _states;

        private readonly float _tickRate = 0;
    
        public BaseState<TContext> CurrentState { get; private set; }

        protected TContext Context;

        public StateMachine(TContext context, params BaseState<TContext>[] states)
        {
            Context = context;
            _states = new Dictionary<Type, BaseState<TContext>>(states.Length);

            foreach (var state in states)
            {
                _states.Add(state.GetType(), state);
            }

            TickAsync();
        }

        public void SwitchState<TState>() where TState : State<TContext>
        {
            CurrentState?.Exit();

            TState newState = GetState<TState>();

            CurrentState = newState;
            
            newState?.Enter();
        }
        
        public void SwitchState<TState, T0>(T0 arg0) where TState : StateOneParam<TContext, T0>
        {
            CurrentState?.Exit();

            TState newState = GetState<TState>();
            
            CurrentState = newState;
            
            newState.Enter(arg0);
        }
        
        public void SwitchState<TState, T0, T1>(T0 arg0, T1 arg1) where TState : StateTwoParam<TContext, T0, T1>
        {
            CurrentState?.Exit();

            TState newState = GetState<TState>();
            
            CurrentState = newState;
            
            newState.Enter(arg0, arg1);
        }

        private async void TickAsync()
        {
            while (true)
            {
                if (_tickRate == 0)
                {
                    await Task.Yield();
                }
                else
                {
                    await Task.Delay((int) (_tickRate * 1000));
                }
                
                CurrentState?.Tick();
            }
        }

        private TState GetState<TState>() where TState : BaseState<TContext>
        {
            return _states[typeof(TState)] as TState;
        }
    }
}