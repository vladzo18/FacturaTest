using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.Enemies.EnemyStateBehaviors
{
    public class TrianglePatrol : IEnumerator<Vector3>
    {
        private readonly Vector3 _startPositio;
        private readonly List<Vector3> _patrolFigurePoints = new()
        {
            new Vector3(-1, 0, -1),
            new Vector3(1, 0, -1),
            new Vector3(0, 0, 1)
        };

        private int _currentIndex = -1;  
        
        public TrianglePatrol(Vector3 startPosition)
        {
            _startPositio = startPosition;
            
            for (int i = 0; i < _patrolFigurePoints.Count; i++)
            {
                _patrolFigurePoints[i] += _startPositio;
            }

            _currentIndex = Random.Range(0, _patrolFigurePoints.Count);
        }
        
        public bool MoveNext()
        {
            _currentIndex++;

            if (_currentIndex >= _patrolFigurePoints.Count)
            {
                _currentIndex = 0; 
            }
            return true;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public Vector3 Current => _patrolFigurePoints[_currentIndex];

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}