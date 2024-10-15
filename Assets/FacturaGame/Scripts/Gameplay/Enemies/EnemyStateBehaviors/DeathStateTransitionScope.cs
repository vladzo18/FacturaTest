using FacturaGame.Scripts.Gameplay.Root.StateMachine;

namespace FacturaGame.Scripts.Gameplay.Enemies.EnemyStateBehaviors
{
    public class DeathStateTransitionScope : StateTransitionScope
    {
        public bool NeedDeathParticles { get; private set; }

        public DeathStateTransitionScope(bool needDeathParticles)
        {
            NeedDeathParticles = needDeathParticles;
        }
    }
}