using FacturaGame.Scripts.Gameplay.Car;
using FacturaGame.Scripts.Gameplay.GameplayStates;
using FacturaGame.Scripts.Gameplay.GameRules;
using FacturaGame.Scripts.Gameplay.Root.GameStates;
using FacturaGame.Scripts.Gameplay.Root.StateMachine;
using FacturaGame.Scripts.Gameplay.Services.InputService;
using FacturaGame.Scripts.Gameplay.Services.StoperService;
using FacturaGame.Scripts.Gameplay.UI;
using FacturaGame.Scripts.GameRoot;
using FacturaGame.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace FacturaGame.Scripts.Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneContext _sceneContext;
        [SerializeField] private Transform _carStartPoint;
        [SerializeField] private CollisionObserver _finishCollisionObserver;

        private readonly BaseStateMachine _gameStateMachine = new ();
        private readonly BaseStateMachine _gameplayStateMachine = new ();
        
        public void Enter()
        {
            InitializeView();
            CreateGameStates();
            CreateGameplayStates();
            
            _gameStateMachine.Enter<GameRunningState>();
            _gameplayStateMachine.Enter<CarInGarageState>();
        }

        private void InitializeView()
        {
            UIRootSceneView rootView = _sceneContext.Container.Resolve<UIRootSceneView>();
            WorldSpaceView worldView = _sceneContext.Container.Resolve<WorldSpaceView>();
            GameplaySceneUI sceneUI = _sceneContext.Container.Resolve<GameplaySceneUI>();
            
            sceneUI.HideAll();
            
            rootView.AttachSceneUI(sceneUI.gameObject);
            rootView.AttachSceneUI(worldView.gameObject);
        }

        private void CreateGameStates()
        {
            CarView carView = _sceneContext.Container.Resolve<CarView>();
            
            IGameRule loseRule = new DefeatRule(carView.HealthCarrier);
            IGameRule victoryRule = new VictoryRule(_finishCollisionObserver);

            GameplaySceneUI gameplaySceneUI = _sceneContext.Container.Resolve<GameplaySceneUI>();
            SystemsStopService systemsStopService = _sceneContext.Container.Resolve<SystemsStopService>();
            IInputService inputService = _sceneContext.Container.Resolve<IInputService>();
            
            GameRunningState gameRunningState = new GameRunningState(_gameStateMachine, loseRule, victoryRule, inputService);
            GameLoseState gameLoseState = new GameLoseState(_gameStateMachine, gameplaySceneUI, _gameplayStateMachine, systemsStopService);
            GameVictoryState gameVictoryState = new GameVictoryState(_gameStateMachine, gameplaySceneUI, _gameplayStateMachine, systemsStopService);
            
            _gameStateMachine.AddState(gameLoseState);
            _gameStateMachine.AddState(gameVictoryState);
            _gameStateMachine.AddState(gameRunningState);
        }
        
        private void CreateGameplayStates()
        {
            CarInGarageState carInGarageState = new CarInGarageState(_gameplayStateMachine, _carStartPoint);
            CarRunningState carRunningState = new CarRunningState(_gameplayStateMachine);
            
            _sceneContext.Container.Inject(carInGarageState);
            _sceneContext.Container.Inject(carRunningState);

            _gameplayStateMachine.AddState(carRunningState);
            _gameplayStateMachine.AddState(carInGarageState);
        }
    }
}
