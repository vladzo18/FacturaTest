using FacturaGame.Scripts.Gameplay.Enemies.EnemyStateBehaviors;
using FacturaGame.Scripts.Gameplay.Root.StateMachine;
using FacturaGame.Scripts.Gameplay.Services.StoperService;
using UnityEngine;
using Zenject;

namespace FacturaGame.Scripts.Gameplay.Enemies
{
    public class EnemyBehaviour : MonoBehaviour, IStopable
    {
        [SerializeField] private EnemyView _enemyView;
        [Header("Patrol Configuration")] 
        [SerializeField] private float _checkRadius;
        [SerializeField] private LayerMask _playerLayerMask;

        private readonly BaseStateMachine _enemyStateMachine = new();
        private ParticleSpawner.ParticleSpawner _particleSpawner;
        private bool _isInitialised = false;
        
        [Inject]
        public void Construcn(SystemsStopService systemsStopService, ParticleSpawner.ParticleSpawner particleSpawner)
        {
            systemsStopService.AddStopable(this);
            _particleSpawner = particleSpawner;
        }
        
        public EnemyBehaviour InitialiseBehavior()
        {
            if (_isInitialised)
            {
                return this;
            }
            
            EnemyPatrolState patrolState = new EnemyPatrolState(_enemyStateMachine, _enemyView, _checkRadius, _playerLayerMask);
            EnemyAttackState attackState = new EnemyAttackState(_enemyStateMachine, _enemyView);
            EnemyDeathState enemyDeathState = new EnemyDeathState(_enemyStateMachine, _enemyView, _particleSpawner);
            
            _enemyStateMachine.AddState(patrolState);
            _enemyStateMachine.AddState(attackState);
            _enemyStateMachine.AddState(enemyDeathState);
            
            _enemyView.HealthCarrier.OnHealthEnded += HealthEnded;

            _isInitialised = true;

            return this;
        }

        public void StartBehavior()
        {
            _enemyStateMachine.Enter<EnemyPatrolState>();
        }
        
        private void HealthEnded()
        {
            _enemyStateMachine.Enter<EnemyDeathState>(new DeathStateTransitionScope(true));
        }

        public void OnStop()
        {
            _enemyStateMachine.Enter<EnemyDeathState>(new DeathStateTransitionScope(false));
        }
    }
}