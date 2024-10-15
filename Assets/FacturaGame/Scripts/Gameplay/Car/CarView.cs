using System.Collections.Generic;
using FacturaGame.Scripts.Gameplay.Health;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.Car
{
    public class CarView : MonoBehaviour
    {
        [SerializeField] private CarMover _carMover;
        [SerializeField] private GunManipulator _gunManipulator;
        [SerializeField] private Transform _healthBarPosition;
        [SerializeField] private HealthCarrier _healthCarrier;
        [SerializeField] private List<TrailRenderer> _wheelTrails;

        public HealthBarView HealthBarView { get; set; }

        private void Start()
        {
            StopTrails();
        }

        public CarMover CarMover => _carMover;
        public GunManipulator GunManipulator => _gunManipulator;
        public Transform HealthBarPosition => _healthBarPosition;
        public HealthCarrier HealthCarrier => _healthCarrier;

        public void RunTrails()
        {
            foreach (var wheelTrail in _wheelTrails)
            {
                wheelTrail.Clear();
                wheelTrail.emitting = true;
            }
        }

        public void StopTrails()
        {
            foreach (var wheelTrail in _wheelTrails)
            {
                wheelTrail.Clear();
                wheelTrail.emitting = false;
            }
        }
    }
}