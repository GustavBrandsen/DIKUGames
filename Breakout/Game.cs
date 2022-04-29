using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout {
	public class Game : DIKUGame {
		private Player player;
		private Level level;
        public Game(WindowArgs windowArgs) : base(windowArgs) {
			BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
			window.SetKeyEventHandler(HandleKeyEvent);

			player = new Player(
				new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.2f, 0.03f)),
				new Image(Path.Combine("Assets", "Images", "player.png")));

			level = new Level("test-lvl.txt");
        }

		public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
				switch (key) {
					case KeyboardKey.Right:
						SendPlayerInput("MoveRightTrue");
						break;
					case KeyboardKey.Left:
						SendPlayerInput("MoveLeftTrue");
						break;
					default:
						break;
				}
			} else if (action == KeyboardAction.KeyRelease) {
				switch (key) {
					case KeyboardKey.Right:
						SendPlayerInput("MoveRightFalse");
						break;
					case KeyboardKey.Left:
						SendPlayerInput("MoveLeftFalse");
						break;
					case KeyboardKey.Escape:
						window.CloseWindow();
						break;
					default:
						break;
				}
			}
        }
		public void SendPlayerInput(string msg) {
			BreakoutBus.GetBus().RegisterEvent (
				new GameEvent {
					EventType = GameEventType.InputEvent,
					From = this,
					To = player,
					Message = msg
				}
			);
		}
		public override void Render() {
            level.Render();
			player.Render();
		}
		public override void Update() {
			BreakoutBus.GetBus().ProcessEventsSequentially();
			player.movePlayer();
		}
    }
}