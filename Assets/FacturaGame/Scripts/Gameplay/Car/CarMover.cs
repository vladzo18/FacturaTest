using FacturaGame.Scripts.Gameplay.Services.StoperService;
using UnityEngine;
using Zenject;

namespace FacturaGame.Scripts.Gameplay.Car
{
    public class CarMover : MonoBehaviour, IStopable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _movingSpeed = 15f;
        [SerializeField] private float _sideSpeed = 2f; 
        [SerializeField] private AnimationCurve _sideMovementCurve;
        [SerializeField] private float _cycleDuration = 5f; 

        private bool _movingRequired;
        private float _timePassed;

        [Inject]
        public void Construct(SystemsStopService systemsStopService)
        {
            systemsStopService.AddStopable(this);
        }
        
        public void FixedUpdate()
        {
            if (_movingRequired)
            {
                Vector3 forwardMovement = transform.forward * _movingSpeed;
                
                _timePassed += Time.fixedDeltaTime;
                float normalizedTime = (_timePassed % _cycleDuration) / _cycleDuration;
                
                float curveValue = _sideMovementCurve.Evaluate(normalizedTime);
                Vector3 sideMovement = transform.right * (curveValue * _sideSpeed);
                
                _rigidbody.velocity = forwardMovement + sideMovement;

                return;
            }

            _rigidbody.velocity = Vector3.zero;
        }

        public void StartMoving()
        {
            _movingRequired = true;
        }

        public void StopMoving()
        {
            _movingRequired = false;
            _timePassed = 0f; 
        }

        public void OnStop()
        {
            StopMoving();
        }
    }
}