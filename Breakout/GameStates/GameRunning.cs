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
using DIKUArcade.Timers;

namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState {
        private Player player;
		private Level level;
        private int levelNum = 0;
        private static GameRunning instance = default!;
		private IBaseImage ballImage;
		private EntityContainer<Ball> balls;
        private Points points;
        private int life;
		private EntityContainer<Life> lives;
        private GameTime gameTime = default!;
        private string[] fileEntries;
        private EntityContainer<PowerUp> powerUps;
        private Dictionary<string, double> timedPowerUps = new Dictionary<string, double>();
        private int infiniteLife;
        public GameRunning() {
			player = new Player(
				new DynamicShape(new Vec2F(0.4f, 0.1f), new Vec2F(0.2f, 0.03f)),
				new Image(Path.Combine("Assets", "Images", "player.png")));

			balls = new EntityContainer<Ball>();
			ballImage = new Image(Path.Combine("Assets", "Images", "Ball.png"));
			fileEntries = Directory.GetFiles(Path.Combine("Assets", "Levels"));

			level = new Level(fileEntries[levelNum]);

            points = new Points(new Vec2F(0.05f,-0.2f), new Vec2F(0.3f,0.3f));

            life = 3;
			lives = new EntityContainer<Life>();
            
			powerUps = new EntityContainer<PowerUp>();
        }
        /// <summary> Make sure you can only create one GameRunning (Defensive programming)</summary>
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

        /// <summary>
		/// Each state can react to key events, delegated from the host StateMachine.
		/// </summary>
		/// <param name="action">Enumeration representing key press/release.</param>
		/// <param name="key">Enumeration representing the keyboard key.</param>
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
                        if (balls.CountEntities() == 0 || timedPowerUps.ContainsKey("infiniteBalls")) {
						    balls.AddEntity(new Ball(
                                new Vec2F(player.GetPosition().X+(player.GetExtent().X/2)-0.02f,player.GetPosition().Y+0.03f),
                                ballImage));
                        }
                        break;
					case KeyboardKey.P:
                        if (gameTime != default!) {
                            gameTime.PauseTimer();
                        }
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

        /// <summary> Count the number of unbreakable blocks in the current level</summary>
        /// <return> Returns number of unbreakable blocks as int</return>
        private int CountUnbreakableBlocks() {
            int count = 0;
            level.Blocks.Iterate(block => {
                if (block.GetType() == typeof(Breakout.UnbreakableBlock)) {
                    count++;
                }
            });
            return count;
        }
        /// <summary> Changes the level of the game and resets multiple objects</summary>
        private void NextMap() {
            if (level.Blocks.CountEntities() == CountUnbreakableBlocks()*2) {
                if (levelNum + 1 < fileEntries.Length) {
                    levelNum++;
                    balls.ClearContainer();
                    player.Shape.Position = new Vec2F(0.4f, 0.1f);
                    level = new Level(fileEntries[levelNum]);
                    CreateGameTimer();
                    timedPowerUps = new Dictionary<string, double>();
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
        /// <summary> Iterates through the ball entitycontainer to move it and other things.</summary>
		private void IterateBalls() {
			balls.Iterate(ball => {
			    ball.Shape.Move();
				if (ball.Shape.Position.Y <= 0.0f) {
					ball.DeleteEntity();
                    if (balls.CountEntities() == 1) {
                        life--;
                        lives.ClearContainer();
                        CreateLives();

                        if(life == 0){
                            ResetState();
                            BreakoutBus.GetBus().RegisterEvent(
                                new GameEvent {
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "GAME_OVER",
                                    StringArg2 = "Lost"
                                }
                            );
                        }
                    }
				} else {
					level.Blocks.Iterate(block => {
                        if (ball.Shape.Position.X <= 0.0f){
                            ball.UpdateDirection(CollisionDirection.CollisionDirLeft);
                            ball.Shape.Position.X = 0.001f;
                        }
                        if (ball.Shape.Position.X >= 0.96f){
                            ball.UpdateDirection(CollisionDirection.CollisionDirRight);
                            ball.Shape.Position.X = 0.959f;
                        }
                        if (ball.Shape.Position.Y >= 0.96f){
                            ball.UpdateDirection(CollisionDirection.CollisionDirUp);
                            ball.Shape.Position.Y = 0.959f;
                        }
					    if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).Collision) {
                            ball.UpdateDirection(
                                CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape).CollisionDir
                                );
						    block.Decreasehealth();
                            if (block.GetHealth() <= 0) {
                                block.DeleteEntity();
                                points.AddPoints(block.GetValue());
                                if (block.CheckPowerUp()) {
                                    ActivatePowerUp(block.Shape.Position);
                                }
                            }
                        }
					    if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape).Collision) {
                            ball.UpdateDirection(
                                CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape).CollisionDir
                                );
                            ball.Shape.Position.Y = player.Shape.Position.Y + player.Shape.AsDynamicShape().Extent.Y;
                        }
                    });
				}
			});
		}
        /// <summary> Iterates through powerups and moves it and other things.</summary>
        private void IteratePowerUps() {
            powerUps.Iterate(powerUp => {
			    powerUp.Shape.Move();
                if (CollisionDetection.Aabb(powerUp.Shape.AsDynamicShape(), player.Shape).Collision) {
                    powerUp.DeleteEntity();
                    if (powerUp.GetType() == typeof(Breakout.ExtraBall)) {
                        if (timedPowerUps.ContainsKey("infiniteBalls")) {
                            timedPowerUps.Remove("infiniteBalls");
                        }
                        timedPowerUps.Add("infiniteBalls", StaticTimer.GetElapsedSeconds()+(double) 3);
                    }
                    if (powerUp.GetType() == typeof(Breakout.ExtraLife) && life < 4) {
                        life++;
                    }
                    if (powerUp.GetType() == typeof(Breakout.ExtraWide) && life < 4) {
                        if (timedPowerUps.ContainsKey("doublePlayerSize")) {
                            timedPowerUps.Remove("doublePlayerSize");
                            player.NormalSize();
                        }
                        timedPowerUps.Add("doublePlayerSize", StaticTimer.GetElapsedSeconds()+(double) 3);
                        player.DoubleSize();
                    }
                    if (powerUp.GetType() == typeof(Breakout.ExtraInvincible)) {
                        if (timedPowerUps.ContainsKey("invincible")) {
                            timedPowerUps.Remove("invincible");
                        }
                        timedPowerUps.Add("invincible", StaticTimer.GetElapsedSeconds()+(double) 3);
                        infiniteLife = life;
                    }
                    if (powerUp.GetType() == typeof(Breakout.ExtraPlayerSpeed)) {
                        if (timedPowerUps.ContainsKey("extraPlayerSpeed")) {
                            timedPowerUps.Remove("extraPlayerSpeed");
                        }
                        timedPowerUps.Add("extraPlayerSpeed", StaticTimer.GetElapsedSeconds()+(double) 3);
                        player.DoubleSpeed();
                    }
                    
                }
                if (powerUp.Shape.Position.Y <= 0.1f) {
                    powerUp.DeleteEntity();
                }
            });
        }
        /// <summary> Check if the timed powerups should be deleted or not</summary>
        private void TimedPowerUps() {
            if (timedPowerUps.ContainsKey("infiniteBalls") && timedPowerUps["infiniteBalls"] <= StaticTimer.GetElapsedSeconds()) {
                timedPowerUps.Remove("infiniteBalls");
            }
            if (timedPowerUps.ContainsKey("doublePlayerSize") && timedPowerUps["doublePlayerSize"] <= StaticTimer.GetElapsedSeconds()) {
                timedPowerUps.Remove("doublePlayerSize");
                player.NormalSize();
            }
            if (timedPowerUps.ContainsKey("invincible")) {
                life = infiniteLife;
                    if (balls.CountEntities() == 0) {
                        balls.AddEntity(new Ball(
                            new Vec2F(player.GetPosition().X+0.083f,player.GetPosition().Y+0.03f),
                            ballImage));
                    }
            }
            if (timedPowerUps.ContainsKey("invincible") && timedPowerUps["invincible"] <= StaticTimer.GetElapsedSeconds()) {
                timedPowerUps.Remove("invincible");
            }
            if (timedPowerUps.ContainsKey("extraPlayerSpeed") && timedPowerUps["extraPlayerSpeed"] <= StaticTimer.GetElapsedSeconds()) {
                timedPowerUps.Remove("extraPlayerSpeed");
                player.NormalSpeed();
            }
        }
        /// <summary> pick a random powerup and add it to the powerup entitycontainer</summary>
	    /// <param name="pos">Vec2F as posistion to be spawned at.</param>
        private void ActivatePowerUp(Vec2F pos) {
            switch (new Random().Next(5)) {
                case 0:
                    powerUps.AddEntity(new ExtraLife(pos));
                    break;
                case 1:
                    powerUps.AddEntity(new ExtraBall(pos));
                    break;
                case 2:
                    powerUps.AddEntity(new ExtraWide(pos));
                    break;
                case 3:
                    powerUps.AddEntity(new ExtraInvincible(pos));
                    break;
                case 4:
                    powerUps.AddEntity(new ExtraPlayerSpeed(pos));
                    break;
            }
            
        }
        /// <summary> Create a gametimer to display time left </summary>
        private void CreateGameTimer() {
            if (level.meta.ContainsKey("Time")) {
                gameTime = new GameTime(Int32.Parse(level.meta["Time"]));
                gameTime.ResetTimer();
            }
        }
        
        ///<summary>Checks if the game time is over and change state if it is </summary>
        private void CheckLostByTime() {
            if (gameTime != default! && gameTime.CheckTimer()) {
                ResetState();
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_OVER",
                        StringArg2 = "Lost"
                    }
                );
            }
        }
        /// <summary> Check if the points is equal or bigger than 100 and then change state</summary>
        private void CheckGameWon() {
            if (points.GetPoints() >= 100) {
                ResetState();
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_OVER",
                        StringArg2 = "Won"
                    }
                );
            }
        }
        /// <summary> Add lives to the entitycontainer </summary>
        private void CreateLives() {
            for (int i = 0; i < life; i++) {
                lives.AddEntity(
                    new Life(
                        new DynamicShape(new Vec2F(0.9f-(i/10f),0.02f), new Vec2F(0.08f,0.08f)),
                        new Image(Path.Combine("Assets", "Images", "heart_filled.png"))
                    )
                );
            }
        }
		/// <summary> Create event that should be sent to the player</summary>
	    /// <param name="msg">String the should be added to the event.</param>
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

        /// <summary>
		/// Render all entities in this GameState
		/// </summary>
		public void RenderState() {
            level.Render();
			player.Render();
			balls.RenderEntities();
            points.Render();
            lives.RenderEntities();
            if (gameTime != default!) {
                gameTime.Render();
            }
            powerUps.RenderEntities();
		}
        /// <summary> Updates by calling multiple methods </summary>
		public void UpdateState() {
			player.MovePlayer();
            IterateBalls();
            NextMap();
            CreateLives();
            CheckLostByTime();
            CheckGameWon();
            IteratePowerUps();
            TimedPowerUps();
		}
    }
}