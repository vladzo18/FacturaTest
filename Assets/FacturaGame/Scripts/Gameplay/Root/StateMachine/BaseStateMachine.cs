using System;
using System.Collections.Generic;

namespace FacturaGame.Scripts.Gameplay.Root.StateMachine
{
    public class BaseStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        private IState _currentState;
        
        public void AddState<T>(T state) where T : IState
        {
            _states.Add(typeof(T), state);
        }
        
        public void Enter<T>(StateTransitionScope transitionScope = null) where T : IState
        {
            if (_states.TryGetValue(typeof(T), out IState state))
            {
                _currentState?.OnExit();
                _currentState = state;
                _currentState.OnEnter(transitionScope);
            }
            else
            {
                throw new ArrayTypeMismatchException();
            }
        }
    }
}