using UnityEngine;

namespace FacturaGame.Scripts.Gameplay
{
    public class CameraFollover : MonoBehaviour
    {
        [SerializeField] private Vector3 offset = new(0, 5, -10);
        [SerializeField] private float smoothSpeed = 0.125f;

        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }
        
        void FixedUpdate()
        {
            if (_target != null)
            {
                Vector3 desiredPosition = new Vector3(transform.position.x, _target.position.y, _target.position.z) + offset;
                
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}