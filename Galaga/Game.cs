using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;

using System.Security.Principal;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.State;

using DIKUArcade.Physics;

using Galaga.Squadron;
using Galaga.MovementStrategy;
using Galaga.GalagaStates;

namespace Galaga {
	public class Game : DIKUGame, IGameEventProcessor {
		private StateMachine stateMachine;
        private MainMenu mainMenu;
        private GameRunning gameRunning;
		public Game(WindowArgs windowArgs) : base(windowArgs) {
            mainMenu = new MainMenu();
            gameRunning = new GameRunning();
			stateMachine = new StateMachine();

			GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.GameStateEvent, GameEventType.WindowEvent});
            GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
			GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
			window.SetKeyEventHandler(HandleKeyEvent);
			
		}
		public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
			stateMachine.ActiveState.HandleKeyEvent(action, key);
        }
		public void ProcessEvent(GameEvent gameEvent) {
			if (gameEvent.EventType == GameEventType.GameStateEvent) {
				stateMachine.ProcessEvent(gameEvent);
			}
			if (gameEvent.EventType == GameEventType.WindowEvent) {
				switch (gameEvent.Message) { 
					case "CLOSE":
						window.CloseWindow();
						break;
					default:
						break;
				} 
			}
		}
		public override void Render() {
			stateMachine.ActiveState.RenderState();
		}
		public override void Update() {
			window.PollEvents();
			GalagaBus.GetBus().ProcessEventsSequentially();
			stateMachine.ActiveState.UpdateState();
		}

	}
}