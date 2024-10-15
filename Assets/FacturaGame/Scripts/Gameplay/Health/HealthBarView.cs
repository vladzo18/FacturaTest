using System;
using FacturaGame.Scripts.Utils.ObjectPooling;
using UnityEngine;
using UnityEngine.UI;

namespace FacturaGame.Scripts.Gameplay.Health
{
    public class HealthBarView : MonoBehaviour, IPoolable
    {
        [SerializeField] private Image _barSlideImage;
        [SerializeField] private Color _playerBarColor;
        [SerializeField] private Color _enemyBarColor;

        private HealthCarrier _healthCarrier;
        
        public HealthBarView Bind(HealthCarrier healthCarrier)
        {
            _healthCarrier = healthCarrier;
            
            _healthCarrier.OnHealthChanged += SetView;
            _healthCarrier.OnHealthEnded += HealthEnded;
            
            return this;
        }

        public HealthBarView SetAsEnemyBar()
        {
            _barSlideImage.color = _enemyBarColor;

            return this;
        }

        public HealthBarView SetAsPlayerBar()
        {
            _barSlideImage.color = _playerBarColor;

            return this;
        }
        
        public HealthBarView HideBar()
        {
            this.gameObject.SetActive(false);

            return this;
        }

        public HealthBarView ShowBar()
        {
            this.gameObject.SetActive(true);
            SetView(_healthCarrier.CurrentHealthValue);
            
            return this;
        }
        private void SetView(float value)
        {
            _barSlideImage.fillAmount = (1 / _healthCarrier.MaxHealthValue) * value;
        }

        private void HealthEnded()
        {
            HideBar();
            Reset();
        }

        #region IPoolable
        public Transform Transform => this.transform;
        public GameObject GameObject => this.gameObject;
        public event Action<IPoolable> OnReturnToPool;
        public void Reset()
        {
            _healthCarrier.OnHealthChanged -= SetView;
            _healthCarrier.OnHealthEnded -= HealthEnded;
            
            OnReturnToPool?.Invoke(this);
        }
        #endregion
    }
}