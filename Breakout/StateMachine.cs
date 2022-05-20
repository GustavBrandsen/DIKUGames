using DIKUArcade.Events;
using DIKUArcade.State;
using System.Collections.Generic;
namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor, IStateMachine {
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
                default:
                    break;
            }
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                if (gameEvent.Message == "CHANGE_STATE") {
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                }
            }
        }
    }
}