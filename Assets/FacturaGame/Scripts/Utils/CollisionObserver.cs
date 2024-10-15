using System;
using UnityEngine;

namespace FacturaGame.Scripts.Utils
{
    public class CollisionObserver : MonoBehaviour
    {
        public event Action<Collision> OnCollided;
        public event Action<Collider> OnTrigered;

        private void OnCollisionEnter(Collision collision)
        {
            OnCollided?.Invoke(collision);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTrigered?.Invoke(other);
        }
    }
}