using System.Collections;
using FacturaGame.Scripts.Gameplay.Services.StoperService;
using FacturaGame.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace FacturaGame.Scripts.Gameplay.Enemies
{
    public class EnemyMover : MonoBehaviour, IStopable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _baseSpeed = 20f;

        public bool IsMoving { get; private set; }
        
        private Coroutine _movingCoroutine;

        [Inject]
        public void Construct(SystemsStopService systemsStopService)
        {
            systemsStopService.AddStopable(this);
        }
        
        public void MoveToPoint(Vector3 point, float baseSpeedMultiplier = 1)
        {
            IsMoving = true;
            StopAnyMovement();
            _movingCoroutine = CoroutineRunner.RunCoroutine(MoveToPointCoroutine(point, baseSpeedMultiplier));
        }

        public void MoveToTarget(Transform target, float baseSpeedMultiplier = 1)
        {
            IsMoving = true;
            StopAnyMovement();
            _movingCoroutine = CoroutineRunner.RunCoroutine(MoveToTargetCoroutine(target, baseSpeedMultiplier));
        }

        private IEnumerator MoveToPointCoroutine(Vector3 point, float baseSpeedMultiplier)
        {
            WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
            
            this.transform.LookAt(point);
            
            while (true)
            {
                if ((this.transform.position - point).sqrMagnitude > 0.01f)
                {
                    _rigidbody.AddForce(transform.forward * (_baseSpeed * baseSpeedMultiplier), ForceMode.Force);
                }
                else
                {
                    _rigidbody.velocity = Vector3.zero;
                    IsMoving = false;
                    break;
                }
                
                yield return fixedUpdate;
            }
        }

        private IEnumerator MoveToTargetCoroutine(Transform target, float baseSpeedMultiplier)
        {
            WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
            
            while (true)
            {
                if ((this.transform.position - target.position).sqrMagnitude > 0.01f)
                {
                    this.transform.LookAt(target.position);
                    _rigidbody.AddForce(transform.forward * (_baseSpeed * baseSpeedMultiplier), ForceMode.Force);
                }
                else
                {
                    _rigidbody.velocity = Vector3.zero;
                    IsMoving = false;
                    break;
                }
                
                yield return fixedUpdate;
            }
        }

        public void StopAnyMovement()
        {
            if (_movingCoroutine != null)
            {
                CoroutineRunner.CancelCoroutine(_movingCoroutine);
                _movingCoroutine = null;
            }
            
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void OnStop()
        {
            StopAnyMovement();
        }
    }
}