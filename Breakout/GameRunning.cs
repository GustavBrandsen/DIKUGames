using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Physics;

namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState {
        private Player player;
		private Level level;
        private int levelNum = 0;
        private static GameRunning instance = default!;
		private IBaseImage ballImage;
		private EntityContainer<Ball> balls;
        private Score score;
        private string[] fileEntries;
        public GameRunning() {
			player = new Player(
				new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.2f, 0.03f)),
				new Image(Path.Combine("Assets", "Images", "player.png")));

			balls = new EntityContainer<Ball>();
			ballImage = new Image(Path.Combine("Assets", "Images", "Ball.png"));
			fileEntries = Directory.GetFiles(Path.Combine("Assets", "Levels"));

			level = new Level(fileEntries[levelNum]);

            score = new Score(new Vec2F(0.05f,-0.2f), new Vec2F(0.3f,0.3f));
        }
        public static GameRunning GetInstance() {
            if (GameRunning.instance == default!) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        public void InitializeGameState() {
            RenderState();
			UpdateState();
            for (int i = 0; i < fileEntries.Length - 1; i++) {
            }
        }
        public void ResetState() {
            instance = default!;
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
					case KeyboardKey.K:
						level.Blocks.ClearContainer();
						break;
					case KeyboardKey.Space:
                        if (balls.CountEntities() == 0) {
						    balls.AddEntity(new Ball(new Vec2F(player.GetPosition().X+0.083f,player.GetPosition().Y+0.03f),ballImage));
                        }
                        break;
					case KeyboardKey.P:
                        BreakoutBus.GetBus().RegisterEvent(
							new GameEvent {
								EventType = GameEventType.GameStateEvent,
								Message = "CHANGE_STATE",
								StringArg1 = "GAME_PAUSED"
							}
						);
                        break;
					case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.InputEvent,
                                Message = "CLOSE"
                            }
                        );
						break;
					default:
						break;
				}
			}
        }
        private void nextMap() {
            if (level.Blocks.CountEntities() == 0) {
                if (levelNum + 1 < fileEntries.Length) {
                    levelNum++;
                    balls.ClearContainer();
                    player.Shape.Position = new Vec2F(0.4f, 0.05f);
                    level = new Level(fileEntries[levelNum]);
                    RenderState();
                } else {
                    ResetState();
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "MAIN_MENU"
                        }
                    );
                }
            }

        }
		private void IterateBalls() {
			balls.Iterate(ball => {
			    ball.Shape.Move();
				if (ball.Shape.Position.Y <= 0.0f) {
					ball.DeleteEntity();
				} else {
					level.Blocks.Iterate(block => {
                        if (ball.Shape.Position.X <= 0.0f){
                            ball.updateDirection(CollisionDirection.CollisionDirLeft);
                            ball.Shape.Position.X = 0.001f;
                        }
                        if (ball.Shape.Position.X >= 0.96f){
                            ball.updateDirection(CollisionDirection.CollisionDirRight);
                            ball.Shape.Position.X = 0.959f;
                        }
                        if (ball.Shape.Position.Y >= 0.96f){
                            ball.updateDirection(CollisionDirection.CollisionDirUp);
                            ball.Shape.Position.Y = 0.959f;
                        }
					    if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).Collision) {
                            ball.updateDirection(CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).CollisionDir);
						    block.decreasehealth();
                            if (block.getHealth() <= 0) {
                                block.DeleteEntity();
                                score.AddPoints(block.getValue());
                            }
                        }
					    if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape).Collision) {
                            ball.updateDirection(CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape).CollisionDir);
                            ball.Shape.Position.Y = player.Shape.Position.Y + player.Shape.AsDynamicShape().Extent.Y;
                        }
                    });
				}
			});
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
		public void RenderState() {
            level.Render();
			player.Render();
			balls.RenderEntities();
            score.Render();
		}
		public void UpdateState() {
			player.movePlayer();
            IterateBalls();
            nextMap();
		}
    }
}