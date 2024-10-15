using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FacturaGame.Scripts.Utils.ObjectPooling
{
    public class ObjectPool
    {
        private static ObjectPool _instance;
        public static ObjectPool Instance => _instance ??= new ObjectPool();
        
        private readonly Dictionary<MonoBehaviour, PoolTask> _activePoolTasks;
        private readonly Transform _objectPoolTransform;

        public event Action OnDisposePool;
        
        private ObjectPool()
        {
            _activePoolTasks = new Dictionary<MonoBehaviour, PoolTask>();
            _objectPoolTransform = (new GameObject()).transform;
            _objectPoolTransform.name = "ObjectPool";
            Object.DontDestroyOnLoad(_objectPoolTransform);
        }
        
        public T GetObject<T>(T prefab) where T : MonoBehaviour, IPoolable 
        {
            if (!_activePoolTasks.TryGetValue(prefab, out var poolTask))
                AddTaskToPool(prefab, out poolTask);
            
            return poolTask.GetFreeObject(prefab);
        }

        private void AddTaskToPool<T>(T prefab, out PoolTask poolTask) where T : MonoBehaviour, IPoolable
        {
            GameObject container = new GameObject
            {
                name = $"{prefab.name}s_pool"
            };
            container.transform.SetParent((_objectPoolTransform));
            poolTask = new PoolTask(container.transform);
            _activePoolTasks.Add(prefab, poolTask);
        }

        public void DisposeTask()
        {
            foreach (var poolTask in _activePoolTasks.Values)
                poolTask.ClearPool();
            
            _activePoolTasks.Clear();
            
            OnDisposePool?.Invoke();
        }
    }
}