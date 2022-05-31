using DIKUArcade.Events;
using DIKUArcade.State;
using System.Collections.Generic;
namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        private string wonLost = default!;
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            ActiveState = MainMenu.GetInstance();
            GameRunning.GetInstance();
        }
        
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.GameOver:
                    ActiveState = GameOver.GetInstance(wonLost);
                    break;
                default:
                    break;
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                if (gameEvent.Message == "CHANGE_STATE") {
                    if (gameEvent.StringArg1 == "GAME_OVER") {
                        wonLost = gameEvent.StringArg2;
                    }
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                }
            }
        }
    }
}