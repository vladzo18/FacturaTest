using System;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.Health
{
    public class HealthCarrier : MonoBehaviour
    {
        [SerializeField] private float _maxHealtx;

        public event Action<float> OnHealthChanged;
        public event Action OnHealthEnded; 

        public float CurrentHealthValue { get; private set; }
        public float MaxHealthValue { get; private set; }

        private void Start()
        {
            CurrentHealthValue = _maxHealtx;
            MaxHealthValue = _maxHealtx;
        }

        public void TakeDamage(float value)
        {
            float resulValue =  CurrentHealthValue - value;
            
            if (resulValue > 0)
            {
                CurrentHealthValue = resulValue;
                OnHealthChanged?.Invoke(CurrentHealthValue);
                
                return;
            }
            
            CurrentHealthValue = 0;
            OnHealthEnded?.Invoke();
        }

        public void Reset()
        {
            CurrentHealthValue = _maxHealtx;
        }
    }
}