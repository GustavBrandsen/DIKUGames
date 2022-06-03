using Breakout;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Events;

namespace Breakout.BreakoutStates{
	public class GameOver : IGameState {
		private static GameOver instance = default!;
		private Entity backGroundImage;
		private Text[] menuButtons;
		private int activeMenuButton;
		private Text gameOverText;
		private System.Drawing.Color gameOverTextColor = default!;
		public GameOver() {
			menuButtons = new Text[] {
				new Text("Main menu", new Vec2F(0.38f,0.2f), new Vec2F(0.4f,0.4f)),
				new Text("Quit", new Vec2F(0.45f,0.1f), new Vec2F(0.4f,0.4f))
			};
			backGroundImage = new Entity(
			    new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
			    new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png"))
			);
			activeMenuButton = 0;
			gameOverText = new Text("You ", new Vec2F(0.40f,0.30f), new Vec2F(0.4f,0.4f));
		}
		
		public static GameOver GetInstance(string msg) {
			if (GameOver.instance == default!) {
				GameOver.instance = new GameOver();
				GameOver.instance.InitializeGameState(msg);
			}
			return GameOver.instance;
		}

		public void InitializeGameState(string msg) {
			RenderState();
			UpdateState();
			gameOverText.SetText("You " + msg);
			if (msg == "Won") {
				gameOverTextColor = System.Drawing.Color.Yellow;
			} else {
				gameOverTextColor = System.Drawing.Color.Red;
			}
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
		public void UpdateState() {
			gameOverText.SetColor(gameOverTextColor);
		}
		
		/// <summary>
		/// Render all entities in this GameState
		/// </summary>
		public void RenderState() {
			backGroundImage.RenderEntity();
			gameOverText.RenderText();

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
									StringArg1 = "MAIN_MENU"
								}
							);
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