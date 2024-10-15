using System;
using System.Collections;
using FacturaGame.Scripts.Gameplay.Enemies;
using FacturaGame.Scripts.Gameplay.Services.StoperService;
using FacturaGame.Scripts.Utils;
using UnityEngine;
using Zenject;
using IPoolable = FacturaGame.Scripts.Utils.ObjectPooling.IPoolable;

namespace FacturaGame.Scripts.Gameplay.Car
{
    public class BulletBehavior : MonoBehaviour, IPoolable, IStopable
    {
        [SerializeField] private CollisionObserver _collisionObserver;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private TrailRenderer _flyingTrail;
        [SerializeField] private float _speed = 25f;
        [SerializeField] private float _lifetime = 1f;
        [SerializeField] private float _damage = 1f;

        private Coroutine _lifetimeCoroutine;
        private Coroutine _flyCoroutine;

        [Inject]
        public void Construct(SystemsStopService systemsStopService)
        {
            systemsStopService.AddStopable(this);
        }
        
        public void StarFly()
        {
            if (_lifetimeCoroutine != null)
            {
                CoroutineRunner.CancelCoroutine(_lifetimeCoroutine);
                _lifetimeCoroutine = null;
            }
            
            _lifetimeCoroutine = CoroutineRunner.RunCoroutine(BulletLifetimeCoroutine());
        }

        private IEnumerator BulletLifetimeCoroutine()
        {
            _collisionObserver.OnCollided += Collided;
            if (_lifetimeCoroutine != null)
            {
                CoroutineRunner.CancelCoroutine(_flyCoroutine);
                _lifetimeCoroutine = null;
            }
            _flyCoroutine = CoroutineRunner.RunCoroutine(FlyCoroutine());
            
            yield return new WaitForSeconds(_lifetime);
            
            _collisionObserver.OnCollided -= Collided;
            CoroutineRunner.CancelCoroutine(_flyCoroutine);
            _flyCoroutine = null;
            Reset();
        }

        private void Collided(Collision collider)
        {
            if (collider.transform.TryGetComponent(out EnemyView enemyView))
            {
                enemyView.HealthCarrier.TakeDamage(_damage);
                enemyView.HealthBarView.ShowBar();
                
                _collisionObserver.OnCollided -= Collided;
                
                CoroutineRunner.CancelCoroutine(_lifetimeCoroutine);
                _lifetimeCoroutine = null;
                
                CoroutineRunner.CancelCoroutine(_flyCoroutine);
                _flyCoroutine = null;
                
                Reset();
            }
        }

        private IEnumerator FlyCoroutine()
        {
            WaitForFixedUpdate wait = new WaitForFixedUpdate();
            
            _flyingTrail.emitting = true;
            
            while (true)
            {
                _rigidbody.velocity = this.transform.forward * _speed;
                _rigidbody.angularVelocity = Vector3.zero;
                yield return wait;
            }
        }

        public void OnStop()
        {
            _collisionObserver.OnCollided -= Collided;
            CoroutineRunner.CancelCoroutine(_lifetimeCoroutine);
            _lifetimeCoroutine = null;
            CoroutineRunner.CancelCoroutine(_flyCoroutine);
            _flyCoroutine = null;
            Reset();
        }

        #region IPoolaple

        public Transform Transform => this.transform;

        public GameObject GameObject => this.gameObject;

        public event Action<IPoolable> OnReturnToPool;

        public void Reset()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _flyingTrail.Clear();
            _flyingTrail.emitting = false;
            
            OnReturnToPool?.Invoke(this);
        }

        #endregion
    }
}