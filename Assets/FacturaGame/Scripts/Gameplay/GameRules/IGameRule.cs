using System;

namespace FacturaGame.Scripts.Gameplay.GameRules
{
    public interface IGameRule
    {
        public event Action OnRuleSatisfied;
    }
}