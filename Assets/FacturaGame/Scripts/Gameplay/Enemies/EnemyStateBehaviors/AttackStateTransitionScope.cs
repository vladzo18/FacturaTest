using FacturaGame.Scripts.Gameplay.Root.StateMachine;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.Enemies.EnemyStateBehaviors
{
    public class AttackStateTransitionScope : StateTransitionScope
    {
        public Transform TargetTransform { get; }

        public AttackStateTransitionScope(Transform target) : base()
        {
            TargetTransform = target;
        }
    }
}