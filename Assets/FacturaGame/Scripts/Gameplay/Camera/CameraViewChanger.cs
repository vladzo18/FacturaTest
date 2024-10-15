using System;
using DG.Tweening;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay
{
    public class CameraViewChanger : MonoBehaviour
    {
        [SerializeField] private ViewConfiguration _garagConfiguration;
        [SerializeField] private ViewConfiguration _movingConfiguration;
        [SerializeField] private float _cameraMoveDuration = 0.5f;
        
        private Transform _target;
        
        public void SetTarget(Transform target)
        {
            _target = target;
        }
        
        public void ToGarageView(bool isAnimatedMoving = false, Action OnChangeViewEnded = null)
        {
            SetView(_garagConfiguration, isAnimatedMoving, OnChangeViewEnded);
        }

        public void ToMovingView(bool isAnimatedMoving = false, Action OnChangeViewEnded = null)
        {
            SetView(_movingConfiguration, isAnimatedMoving, OnChangeViewEnded);
        }

        private void SetView(ViewConfiguration viewConfiguration, bool isAnimatedMoving, Action OnChangeViewEnded)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Join(transform.DOMove( _target.position + viewConfiguration.Position, isAnimatedMoving ? _cameraMoveDuration : 0f));
            sequence.Join(transform.DORotate(viewConfiguration.Rotation, isAnimatedMoving ? _cameraMoveDuration : 0f));

            sequence.OnComplete(() => OnChangeViewEnded?.Invoke());

            sequence.Play();
        }
        
        [Serializable]
        private class ViewConfiguration
        {
            public Vector3 Position;
            public Vector3 Rotation;
        }
    }
}
