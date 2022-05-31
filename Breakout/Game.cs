using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Breakout.BreakoutStates;

namespace Breakout {
	public class Game : DIKUGame, IGameEventProcessor {
		private StateMachine stateMachine;
        public Game(WindowArgs windowArgs) : base(windowArgs) {
			stateMachine = new StateMachine();

			BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
				GameEventType.InputEvent, GameEventType.GameStateEvent
				});
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
			BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
			window.SetKeyEventHandler(HandleKeyEvent);
        }
		
		public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            stateMachine.ActiveState.HandleKeyEvent(action, key);
        }

		public void ProcessEvent(GameEvent gameEvent) {
			if (gameEvent.EventType == GameEventType.GameStateEvent) {
				stateMachine.ProcessEvent(gameEvent);
			}
            if (gameEvent.EventType == GameEventType.InputEvent) {
                if (gameEvent.Message == "CLOSE") {
                    window.CloseWindow();
                }
            }
        }

		public override void Render() {
			stateMachine.ActiveState.RenderState();
		}

		public override void Update() {
			stateMachine.ActiveState.UpdateState();
			BreakoutBus.GetBus().ProcessEventsSequentially();
		}
    }
}