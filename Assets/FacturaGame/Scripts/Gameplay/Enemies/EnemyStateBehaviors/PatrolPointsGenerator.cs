using System.Collections.Generic;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.Enemies.EnemyStateBehaviors
{
    public class PatrolPointsGenerator 
    {
        private readonly IEnumerator<Vector3> _patrullEnumeratot;
        
        public PatrolPointsGenerator(Transform generationTarget)
        {
            _patrullEnumeratot = new TrianglePatrol(generationTarget.position);
        }

        public Vector3 GetPoint()
        {
            _patrullEnumeratot.MoveNext();
            return _patrullEnumeratot.Current;
        }
    }
}