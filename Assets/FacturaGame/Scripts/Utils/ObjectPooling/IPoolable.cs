using System;
using UnityEngine;

namespace FacturaGame.Scripts.Utils.ObjectPooling
{
    public interface IPoolable
    {
        public Transform Transform { get; }
        public GameObject GameObject { get; }
        public event Action<IPoolable> OnReturnToPool;
        public void Reset();
    }
}