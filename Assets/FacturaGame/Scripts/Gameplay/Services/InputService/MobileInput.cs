using System;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.Services.InputService
{
    public class MobileInput : MonoBehaviour, IInputService
    {
        private const float MaxSwipeSpeed = 2500f;
        private const float InputSwitchedThreshold = 0.2f;
        
        public bool IsInputRequired { get; set; }
        
        public event Action OnTaped;
        public event Action<float> OnHorizontalMove;
        
        private Vector2 _startTouchPosition;
        private bool _isSwiping = false;
        private float _timeAfterInputSwitched;
        
        public void Update()
        {
            if (!IsInputRequired)
            {
                _timeAfterInputSwitched = 0;
                return;
            }
            if (_timeAfterInputSwitched < InputSwitchedThreshold)
            {
                _timeAfterInputSwitched += Time.deltaTime;
                return;
            }
            
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startTouchPosition = touch.position;
                        _isSwiping = true;
                        break;
                
                    case TouchPhase.Moved:
                        if (_isSwiping)
                        {
                            Vector2 currentTouchPosition = touch.position;
                            float swipeDistance = currentTouchPosition.x - _startTouchPosition.x;
                            float swipeSpeed = swipeDistance / Time.deltaTime;
                            float normalizedSwipe = Mathf.Clamp(swipeSpeed / MaxSwipeSpeed, -1f, 1f);
                            
                            this.OnHorizontalMove?.Invoke(normalizedSwipe);
                            
                            _startTouchPosition = currentTouchPosition;
                        }
                        break;

                    case TouchPhase.Ended:
                        if (_isSwiping)
                        {
                            this.OnTaped?.Invoke();
                            _isSwiping = false;
                        }
                        break;
                }
            }
        }
    }
}
