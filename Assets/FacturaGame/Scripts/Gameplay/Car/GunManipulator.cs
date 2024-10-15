using System.Collections;
using FacturaGame.Scripts.Gameplay.Services.StoperService;
using FacturaGame.Scripts.Utils;
using FacturaGame.Scripts.Utils.ObjectPooling;
using UnityEngine;
using Zenject;

namespace FacturaGame.Scripts.Gameplay.Car
{
    public class GunManipulator : MonoBehaviour, IStopable
    {
        [SerializeField] private GameObject _gunObject;
        [SerializeField] private Vector2Int _requiredRotation = new(-45, 45);
        [SerializeField] private float _fireCooldown = 0.3f;
        [SerializeField] private LineRenderer _laser;
        [SerializeField] private BulletBehavior bulletBehaviorPrefab;
        [SerializeField] private Transform _bulletStartPosition;

        private Quaternion _defaultRotation;
        private Coroutine _fireCoroutine;

        [Inject]
        public void Construct(SystemsStopService systemsStopService)
        {
            systemsStopService.AddStopable(this);
        }
        
        public void Start()
        {
            _defaultRotation = _gunObject.transform.rotation;
        }

        public void Rotate(float value)
        {
            float currentRotation = _gunObject.transform.rotation.eulerAngles.z;
            float result = currentRotation + value;

            if (result >= _requiredRotation.x || result <= _requiredRotation.y)
            {
                _gunObject.transform.Rotate(Vector3.back, value);
            }
        }

        public void ResetRotation()
        {
            _gunObject.transform.rotation = _defaultRotation;
        }

        public void StartFire()
        {
            _fireCoroutine = CoroutineRunner.RunCoroutine(FireCoroutine());
        }
        
        public void StopFire()
        {
            CoroutineRunner.CancelCoroutine(_fireCoroutine);
        }

        public void SetLaserVisibility(bool isVisible)
        {
            _laser.enabled = isVisible;
        }

        private IEnumerator FireCoroutine()
        {
            WaitForSeconds waitFireCooldown = new WaitForSeconds(_fireCooldown);
            
            while (true)
            {
                yield return waitFireCooldown;
                
                BulletBehavior bulletBehavior = ObjectPool.Instance.GetObject(bulletBehaviorPrefab);
                bulletBehavior.Transform.position = _bulletStartPosition.position;
                bulletBehavior.Transform.rotation = Quaternion.Euler(0, _gunObject.transform.rotation.eulerAngles.y, 0);
                bulletBehavior.StarFly();
            }
        }

        public void OnStop()
        {
            StopFire();
        }
    }
}