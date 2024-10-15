using FacturaGame.Scripts.Gameplay.Car;
using FacturaGame.Scripts.Gameplay.Root.StateMachine;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.Enemies.EnemyStateBehaviors
{
    public class EnemyAttackState : IState
    {
        private const float AttackRunSpeedMultiplier = 3f;
        
        private readonly EnemyView _enemyView;
        private readonly BaseStateMachine _stateMachine;
        
        public EnemyAttackState(BaseStateMachine stateMachine, EnemyView enemyView)
        {
            _stateMachine = stateMachine;
            _enemyView = enemyView;
        }
        
        public void OnEnter(StateTransitionScope transitionScope)
        {
            AttackStateTransitionScope scope = (AttackStateTransitionScope) transitionScope;
            
            _enemyView.EnemyMover.MoveToTarget(scope.TargetTransform, AttackRunSpeedMultiplier);
            _enemyView.EnemyAnimator.AnimateRun();
            
            _enemyView.CollisionObserver.OnCollided += CollidedHandler;
        }

        private void CollidedHandler(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out CarView carView))
            {
               carView.HealthCarrier.TakeDamage(_enemyView.HealthCarrier.CurrentHealthValue);
               _stateMachine.Enter<EnemyDeathState>(new DeathStateTransitionScope(true));
            }
        }

        public void OnExit()
        {
            _enemyView.CollisionObserver.OnCollided -= CollidedHandler;
        }
    }
}
