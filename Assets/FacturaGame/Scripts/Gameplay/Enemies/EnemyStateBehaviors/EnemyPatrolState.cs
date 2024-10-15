using System.Collections;
using FacturaGame.Scripts.Gameplay.Root.StateMachine;
using FacturaGame.Scripts.Utils;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.Enemies.EnemyStateBehaviors
{
    public class EnemyPatrolState : IState
    {
        private readonly BaseStateMachine _stateMachine;
        private readonly EnemyView _enemyView;
        private readonly float _checkRadius;
        private readonly LayerMask _playerMask;
        
        private Coroutine _patrolCoroutine;
        private Coroutine _checkForTargetCoroutine;

        public EnemyPatrolState(BaseStateMachine stateMachine, EnemyView enemyView, float checkRadius, LayerMask playerMask)
        {
            _stateMachine = stateMachine;
            _enemyView = enemyView;
            _checkRadius = checkRadius;
            _playerMask = playerMask;
        }
        
        public void OnEnter(StateTransitionScope transitionScope)
        {
            _enemyView.EnemyAnimator.AnimateWalk();
            
            _patrolCoroutine = CoroutineRunner.RunCoroutine(PatrulCoroutine());
            _checkForTargetCoroutine = CoroutineRunner.RunCoroutine(CheckForTarget());
        }

        public void OnExit()
        {
            if (_patrolCoroutine != null)
            {
                CoroutineRunner.CancelCoroutine(_patrolCoroutine);
                _patrolCoroutine = null;
            }
            
            if (_checkForTargetCoroutine != null)
            {
                CoroutineRunner.CancelCoroutine(_checkForTargetCoroutine);
                _checkForTargetCoroutine = null;
            }
        }

        private IEnumerator PatrulCoroutine()
        {
            PatrolPointsGenerator pointsGenerator = new PatrolPointsGenerator(_enemyView.Transform);
            WaitWhile waitStopMoving = new WaitWhile(() => _enemyView.EnemyMover.IsMoving);
            
            while (true)
            {
                _enemyView.EnemyMover.MoveToPoint(pointsGenerator.GetPoint());
                
                yield return waitStopMoving;
            }
        }

        private IEnumerator CheckForTarget()
        {
            Collider[] colliders = null;
            
            while (true)
            {
                colliders = Physics.OverlapSphere( _enemyView.Transform.position, _checkRadius, _playerMask);
                
                if (colliders != null && colliders.Length > 0)
                {
                   break;
                }
                
                yield return new WaitForSeconds(0.1f);
            }
            
            _stateMachine.Enter<EnemyAttackState>(new AttackStateTransitionScope(colliders[0].transform));
        }
    }
}