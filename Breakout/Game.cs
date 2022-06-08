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
		/// <summary> Registers an event and sends it to the active states HandleKeyEvent method </summary>
		/// <param name="action">Enumeration representing key press/release.</param>
		/// <param name="key">Enumeration representing the keyboard key.</param>
		public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            stateMachine.ActiveState.HandleKeyEvent(action, key);
        }
		/// <summary> Registers events from the Breakoutbus and handle appropriate</summary>
		/// <param name="gameEvent">A GameEvent in the Breakoutbus.</param>
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
		/// <summary> Render the active state </summary>
		public override void Render() {
			stateMachine.ActiveState.RenderState();
		}
		/// <summary> Updates the active state </summary>
		public override void Update() {
			stateMachine.ActiveState.UpdateState();
			BreakoutBus.GetBus().ProcessEventsSequentially();
		}
    }
}