using System;
using FacturaGame.Scripts.Gameplay.Car;
using FacturaGame.Scripts.Utils;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.GameRules
{
    public class VictoryRule : IGameRule
    {
        private readonly CollisionObserver _finishCollisionObserver;
        
        public event Action OnRuleSatisfied;

        public VictoryRule(CollisionObserver finishCollisionObserver)
        {
            _finishCollisionObserver = finishCollisionObserver;
            _finishCollisionObserver.OnTrigered += OnFinishTriggered;
        }

        private void OnFinishTriggered(Collider collider)
        {
            if (collider.transform.TryGetComponent(out CarView carView))
            {
                OnRuleSatisfied?.Invoke();  
            }
        }
    }
}