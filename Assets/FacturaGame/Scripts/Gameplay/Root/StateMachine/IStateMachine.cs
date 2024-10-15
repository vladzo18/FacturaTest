namespace FacturaGame.Scripts.Gameplay.Root.StateMachine
{
    public interface IStateMachine
    {
        public void AddState<T>(T state) where T : IState;
        public void Enter<T>(StateTransitionScope transitionScope) where T : IState;
    }
}