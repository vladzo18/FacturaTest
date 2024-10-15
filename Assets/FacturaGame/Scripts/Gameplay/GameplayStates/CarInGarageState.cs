using FacturaGame.Scripts.Gameplay.Car;
using FacturaGame.Scripts.Gameplay.Root.StateMachine;
using FacturaGame.Scripts.Gameplay.Services.InputService;
using FacturaGame.Scripts.GameRoot;
using UnityEngine;
using Zenject;

namespace FacturaGame.Scripts.Gameplay.GameplayStates
{
    public class CarInGarageState : IState
    {
        private readonly IStateMachine _stateMachine;
        
        private IInputService _inputService;
        private CameraViewChanger _cameraViewChanger;
        private WorldSpaceView _worldSpaceView;
        private Transform _garagePoint;
        private CarView _carView;

        public CarInGarageState(IStateMachine stateMachine, Transform garagePoint)
        {
            _stateMachine = stateMachine;
            _garagePoint = garagePoint;
        }
        
        [Inject]
        public void Construct(CameraViewChanger cameraViewChanger, CarView carView, IInputService inputService, WorldSpaceView worldSpaceView)
        {
            _inputService = inputService;
            _cameraViewChanger = cameraViewChanger;
            _carView = carView;
            _worldSpaceView = worldSpaceView;
        }
        
        public void OnEnter(StateTransitionScope transitionScope)
        {
            _carView.transform.position = _garagePoint.position;
            
            _cameraViewChanger.SetTarget(_carView.transform);
            _cameraViewChanger.ToGarageView();

            _carView.GunManipulator.SetLaserVisibility(false);
            
            _carView.HealthBarView = _worldSpaceView.
                CreateHealthBar(_carView.HealthBarPosition).
                Bind(_carView.HealthCarrier).
                SetAsPlayerBar().
                HideBar();
            
            
            _inputService.OnTaped += ScreenTaped;

            _inputService.IsInputRequired = true;
        }

        public void OnExit()
        {
            _inputService.OnTaped -= ScreenTaped;
            
            _inputService.IsInputRequired = false;
        }

        private void ScreenTaped()
        {
            _stateMachine.Enter<CarRunningState>(null);
        }
    }
}