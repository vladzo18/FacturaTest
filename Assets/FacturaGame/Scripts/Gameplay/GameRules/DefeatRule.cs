using System;
using FacturaGame.Scripts.Gameplay.Health;

namespace FacturaGame.Scripts.Gameplay.GameRules
{
    public class DefeatRule : IGameRule
    {
        private readonly HealthCarrier _playerHealthCarrier;
        
        public event Action OnRuleSatisfied;
        
        public DefeatRule(HealthCarrier healthCarrier)
        {
            healthCarrier.OnHealthEnded += HealthEndedHandler;
            _playerHealthCarrier = healthCarrier;
        }

        private void HealthEndedHandler()
        {
            OnRuleSatisfied?.Invoke();
        }
    }
}