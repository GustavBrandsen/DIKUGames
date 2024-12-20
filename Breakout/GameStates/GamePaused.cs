using Breakout;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Events;
using DIKUArcade.Timers;

namespace Breakout.BreakoutStates{
	public class GamePaused : IGameState {
		private static GamePaused instance = default!;
		private Entity backGroundImage;
		private Text[] menuButtons;
		private int activeMenuButton;
		private Text gamePaused;
		public GamePaused() {
			menuButtons = new Text[] {
				new Text("Continue", new Vec2F(0.4f,0.2f), new Vec2F(0.4f,0.4f)),
				new Text("Quit", new Vec2F(0.45f,0.1f), new Vec2F(0.4f,0.4f))
			};
			backGroundImage = new Entity(
			    new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
			    new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png"))
			);
			activeMenuButton = 0;

			gamePaused = new Text("Game is Paused!", new Vec2F(0.20f,0.13f), new Vec2F(0.6f,0.6f));
			gamePaused.SetColor(System.Drawing.Color.Turquoise);
		}
		
		public static GamePaused GetInstance() {
			if (GamePaused.instance == default!) {
				GamePaused.instance = new GamePaused();
				GamePaused.instance.InitializeGameState();
			}
			return GamePaused.instance;
		}

		public void InitializeGameState() {
			RenderState();
			UpdateState();
		}

		public void ResetState() {
			instance = default!;
			menuButtons = new Text[]{};
			backGroundImage = new Entity(
				new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.0f, 0.0f)),
			    new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png"))
			);
		}

		/// <summary>
		/// Update all variables that are being used by this GameState.
		/// </summary>
		public void UpdateState() {}
		
		/// <summary>
		/// Render all entities in this GameState
		/// </summary>
		public void RenderState() {
			backGroundImage.RenderEntity();
			gamePaused.RenderText();

			for (int i = 0; i < menuButtons.Length; i++) {
				menuButtons[i].SetColor(System.Drawing.Color.Gray);
				menuButtons[i].RenderText();
				menuButtons[activeMenuButton].SetColor(System.Drawing.Color.White);
				menuButtons[activeMenuButton].RenderText();
			}
			
		}
		
		/// <summary>
		/// Each state can react to key events, delegated from the host StateMachine.
		/// </summary>
		/// <param name="action">Enumeration representing key press/release.</param>
		/// <param name="key">Enumeration representing the keyboard key.</param>
		public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
			if (action == KeyboardAction.KeyPress) {
				switch(key) {
					case KeyboardKey.Escape:
						BreakoutBus.GetBus().RegisterEvent(
							new GameEvent {
								EventType = GameEventType.InputEvent,
								Message = "CLOSE"
							}
						);
						break;
					case KeyboardKey.Up:
						if (activeMenuButton > 0) {
							activeMenuButton--;
							menuButtons[activeMenuButton].SetColor(System.Drawing.Color.White);
							menuButtons[activeMenuButton].RenderText();
						}
						break;
					case KeyboardKey.Down:
						if (activeMenuButton < 1) {
							activeMenuButton++;
							menuButtons[activeMenuButton].SetColor(System.Drawing.Color.White);
							menuButtons[activeMenuButton].RenderText();
						}
						break;
					case KeyboardKey.Enter:
						if (activeMenuButton == 0){
							ResetState();
							BreakoutBus.GetBus().RegisterEvent(
								new GameEvent {
									EventType = GameEventType.GameStateEvent,
									Message = "CHANGE_STATE",
									StringArg1 = "GAME_RUNNING"
								}
							);
							StaticTimer.ResumeTimer();
						} else if (activeMenuButton == 1) {
							BreakoutBus.GetBus().RegisterEvent(
								new GameEvent {
									EventType = GameEventType.InputEvent,
									Message = "CLOSE"
								}
							);
						}
						break;
					default:
						break;
				}
			}
		}
	}
}