using FacturaGame.Scripts.Gameplay.GameRules;
using FacturaGame.Scripts.Gameplay.Root.StateMachine;
using FacturaGame.Scripts.Gameplay.Services.InputService;

namespace FacturaGame.Scripts.Gameplay.Root.GameStates
{
    public class GameRunningState : IState
    {
        private readonly BaseStateMachine _gameStateMachine;
        private readonly IGameRule _loseGame;
        private readonly IGameRule _victoryGame;
        private readonly IInputService _inputService;
        
        public GameRunningState(BaseStateMachine gameStateMachine, IGameRule loseRule, IGameRule victoryRule, IInputService inputService)
        {
            _gameStateMachine = gameStateMachine;
            _loseGame = loseRule;
            _victoryGame = victoryRule;
            _inputService = inputService;
        }
        
        public void OnEnter(StateTransitionScope transitionScope)
        {
            _loseGame.OnRuleSatisfied += OnGameLoseHandler;
            _victoryGame.OnRuleSatisfied += OnGameVictoryHandler;
        }

        public void OnExit()
        {
            _loseGame.OnRuleSatisfied -= OnGameLoseHandler;
            _victoryGame.OnRuleSatisfied -= OnGameVictoryHandler;
        }

        private void OnGameLoseHandler()
        {
            _inputService.IsInputRequired = false;
            _gameStateMachine.Enter<GameLoseState>();
        }

        private void OnGameVictoryHandler()
        {
            _inputService.IsInputRequired = false;
            _gameStateMachine.Enter<GameVictoryState>();
        }
    }
}