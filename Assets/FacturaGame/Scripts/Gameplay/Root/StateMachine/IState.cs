namespace FacturaGame.Scripts.Gameplay.Root.StateMachine
{
    public interface IState
    {
        public void OnEnter(StateTransitionScope transitionScope);
        public void OnExit();
    }
}