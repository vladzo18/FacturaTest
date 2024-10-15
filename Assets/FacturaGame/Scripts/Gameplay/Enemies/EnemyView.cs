using System;
using FacturaGame.Scripts.Gameplay.Health;
using FacturaGame.Scripts.Utils;
using FacturaGame.Scripts.Utils.ObjectPooling;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.Enemies
{
    public class EnemyView : MonoBehaviour, IPoolable
    {
        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private HealthCarrier _healthCarrier;
        [SerializeField] private CollisionObserver _collisionObserver;
        [SerializeField] private EnemyBehaviour _enemyBehaviour;
        [SerializeField] private Transform _healthBarPosition;

        public HealthBarView HealthBarView { get; set; }
        
        public EnemyMover EnemyMover => _enemyMover;
        public EnemyAnimator EnemyAnimator => _enemyAnimator;
        public HealthCarrier HealthCarrier => _healthCarrier;
        public CollisionObserver CollisionObserver => _collisionObserver;
        public EnemyBehaviour EnemyBehaviour => _enemyBehaviour;
        public Transform HealthBarPosition => _healthBarPosition;

        #region IPoolable
        public Transform Transform => this.transform;
        public GameObject GameObject => this.gameObject;
        public event Action<IPoolable> OnReturnToPool;
        public void Reset()
        {
            OnReturnToPool?.Invoke(this);
        }
        #endregion
    }
}