using FacturaGame.Scripts.Gameplay.Root.StateMachine;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.Enemies.EnemyStateBehaviors
{
    public class EnemyDeathState : IState
    {
        private readonly BaseStateMachine _baseStateMachine;
        private readonly EnemyView _enemyView;
        private readonly ParticleSpawner.ParticleSpawner _particleSpawner;

        public EnemyDeathState(BaseStateMachine baseStateMachine, EnemyView enemyView, ParticleSpawner.ParticleSpawner particleSpawner)
        {
            _baseStateMachine = baseStateMachine;
            _enemyView = enemyView;
            _particleSpawner = particleSpawner;
        }

        public void OnEnter(StateTransitionScope transitionScope)
        {
            DeathStateTransitionScope scope = (DeathStateTransitionScope) transitionScope;

            if (scope.NeedDeathParticles)
            {
                _particleSpawner.PlayEnemyDeathAt(_enemyView.Transform.position);
            }
            
            _enemyView.EnemyMover.StopAnyMovement();
            _enemyView.HealthBarView.HideBar();
            _enemyView.HealthCarrier.Reset();
            _enemyView.Transform.position = Vector3.zero;
           
           _enemyView.Reset();
        }

        public void OnExit() { }
    }
}