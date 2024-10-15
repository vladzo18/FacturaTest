using System;

namespace FacturaGame.Scripts.Gameplay.Services.InputService
{
    public interface IInputService
    {
        public event Action OnTaped;
        public event Action<float> OnHorizontalMove;
        public bool IsInputRequired { get; set; }
    }
}