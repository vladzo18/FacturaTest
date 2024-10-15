using System.Collections.Generic;
using FacturaGame.Scripts.Gameplay.Health;
using FacturaGame.Scripts.Utils.ObjectPooling;
using UnityEngine;

namespace FacturaGame.Scripts.GameRoot
{
    public class WorldSpaceView : MonoBehaviour
    {
        [SerializeField] private Transform _worldSpaceUIContentContainer;
        [SerializeField] private HealthBarView _healthBarPrefab;

        private List<(HealthBarView, Transform)> _positionUpdatables = new ();
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            foreach (var positionUpdatable in _positionUpdatables)
            {
                positionUpdatable.Item1.transform.position = positionUpdatable.Item2.position;
                positionUpdatable.Item1.transform.LookAt(_mainCamera.transform.position);
            }
        }

        public HealthBarView CreateHealthBar(Transform target)
        {
            HealthBarView healthBarView = ObjectPool.Instance.GetObject(_healthBarPrefab);
            healthBarView.Transform.position = target.position;
            _positionUpdatables.Add((healthBarView, target));
            AttachUI(healthBarView.gameObject);
            return healthBarView;
        }
        
        private void AttachUI(GameObject ui)
        {
            ui.transform.SetParent(_worldSpaceUIContentContainer);
        }
    }
}