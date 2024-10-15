using FacturaGame.Scripts.Gameplay.Car;
using FacturaGame.Scripts.Gameplay.Enemies;
using FacturaGame.Scripts.Gameplay.Root.StateMachine;
using FacturaGame.Scripts.Gameplay.Services.InputService;
using Zenject;

namespace FacturaGame.Scripts.Gameplay.GameplayStates
{
    public class CarRunningState : IState
    {
        private readonly IStateMachine _stateMachine;
        
        private IInputService _inputService;
        private CameraViewChanger _cameraViewChanger;
        private CarView _carView;
        private CameraFollover _cameraFollover;
        private EnemySpawner _enemySpawner;
        
        public CarRunningState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        [Inject]
        public void Construct(CameraViewChanger cameraViewChanger, CarView carView, IInputService inputService, CameraFollover cameraFollover, EnemySpawner enemySpawner)
        {
            _inputService = inputService;
            _cameraViewChanger = cameraViewChanger;
            _carView = carView;
            _cameraFollover = cameraFollover;
            _enemySpawner = enemySpawner;
        }
        
        public void OnEnter(StateTransitionScope transitionScope)
        {
            _cameraViewChanger.SetTarget(_carView.transform);
            
            _cameraViewChanger.ToMovingView(true, () =>
            {
                _cameraFollover.SetTarget(_carView.transform);
                _carView.CarMover.StartMoving();
                _carView.GunManipulator.StartFire();
                _carView.GunManipulator.SetLaserVisibility(true);
                _carView.HealthBarView.ShowBar();
            });
            
            _enemySpawner.SetSpawnTarget(_carView.transform);
            _enemySpawner.RunSpawn();
            _carView.RunTrails();
            
            _inputService.OnHorizontalMove += HorizontalMoved;

            _inputService.IsInputRequired = true;
        }

        public void OnExit()
        {
            _cameraFollover.SetTarget(null);
            _carView.CarMover.StopMoving();
            _carView.GunManipulator.StopFire();
            _carView.GunManipulator.ResetRotation();
            _enemySpawner.StopSpawn();
            _carView.HealthCarrier.Reset();
            _carView.StopTrails();
            _inputService.OnHorizontalMove -= HorizontalMoved;
            
            _inputService.IsInputRequired = false;
        }

        private void HorizontalMoved(float value)
        {
            _carView.GunManipulator.Rotate(value * 2.25f);
        }
    }
}