using FacturaGame.Scripts.Gameplay.GameplayStates;
using FacturaGame.Scripts.Gameplay.Root.StateMachine;
using FacturaGame.Scripts.Gameplay.Services.StoperService;
using FacturaGame.Scripts.Gameplay.UI;

namespace FacturaGame.Scripts.Gameplay.Root.GameStates
{
    public class GameLoseState : IState
    {
        private readonly BaseStateMachine _gameStateMachine;
        private readonly GameplaySceneUI _gameplaySceneUI;
        private readonly BaseStateMachine _carStateMachine;
        private readonly SystemsStopService _systemsStopService;

        public GameLoseState(BaseStateMachine gameStateMachine, GameplaySceneUI gameplaySceneUI, BaseStateMachine carStateMachine, SystemsStopService  systemsStopService)
        {
            _gameStateMachine = gameStateMachine;
            _gameplaySceneUI = gameplaySceneUI;
            _carStateMachine = carStateMachine;
            _systemsStopService = systemsStopService;
        }

        public void OnEnter(StateTransitionScope transitionScope)
        {
            _systemsStopService.StopStopables();
            
            _gameplaySceneUI.HideAll();
            _gameplaySceneUI.ShowLoseWindow();

            _gameplaySceneUI.OnRestartClick += RestartClicked;
        }

        public void OnExit()
        {
            _gameplaySceneUI.HideAll();
            _carStateMachine.Enter<CarInGarageState>();
        }

        private void RestartClicked()
        {
            _gameStateMachine.Enter<GameRunningState>();
        }
    }
}