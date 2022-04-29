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

namespace Galaga.GalagaStates {
    public class GameRunning : IGameState {
        private Player player;
		private EntityContainer<Enemy> enemies;
		private List<Image> enemyStridesRed;
		private EntityContainer<PlayerShot> playerShots;
		private IBaseImage playerShotImage;
		private AnimationContainer enemyExplosions;
		private List<Image> explosionStrides;
		private const int EXPLOSION_LENGTH_MS = 500;
		private ISquadron squadPicker;
		private Score score;
		private Text gameOver;
		private IMovementStrategy moveStratPicker;
        private static GameRunning instance = null;
        public GameRunning() {
            gameOver = new Text ("", new Vec2F(0.3f,0.1f), new Vec2F(0.5f,0.5f));

			playerShots = new EntityContainer<PlayerShot>();
			playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

			score = new Score(new Vec2F(0.05f,-0.2f), new Vec2F(0.3f,0.3f));

			var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
			enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets","Images", "RedMonster.png"));
			const int numEnemies = 8;
			enemies = new EntityContainer<Enemy>(numEnemies);
			playerShots = new EntityContainer<PlayerShot>();

			SquadPick();
			MovePick();
			squadPicker.CreateEnemies(images, enemyStridesRed);
			
			player = new Player(
				new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
				new Image(Path.Combine("Assets", "Images", "Player.png")));

			GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, player);

			enemyExplosions = new AnimationContainer(numEnemies);
			explosionStrides = ImageStride.CreateStrides(8,
				Path.Combine("Assets", "Images", "Explosion.png"));
		}
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
        }
        public void InitializeGameState() {
            RenderState();
			UpdateState();
        }
        public void ResetState() {
            instance = null;

        }

        /// <summary>
        /// Update all variables that are being used by this GameState.
        /// </summary>
        public void UpdateState() {
			player.move();
			IterateShots();
			moveStratPicker.MoveEnemies(squadPicker.Enemies);
			gameover();
			GalagaBus.GetBus().ProcessEventsSequentially();
        }
        
        /// <summary>
        /// Render all entities in this GameState
        /// </summary>
        public void RenderState() {
            player.Render();
			squadPicker.Enemies.RenderEntities();
			playerShots.RenderEntities();
			enemyExplosions.RenderAnimations();
			score.RenderScore();
			gameOver.RenderText();
        }
        
        /// <summary>
        /// Each state can react to key events, delegated from the host StateMachine.
        /// </summary>
        /// <param name="action">Enumeration representing key press/release.</param>
        /// <param name="key">Enumeration representing the keyboard key.</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
				KeyPress(key);
			} else if (action == KeyboardAction.KeyRelease) {
				KeyRelease(key);
			}
        }
        ///<summary> Sends a message to RegisterPlayerEvent to move player </summary>
		/// <param> Takes a key as a parameter</param>
		/// <output> Nothing</output> 
		public void KeyPress(KeyboardKey key) {
			switch (key) {
				case KeyboardKey.Right:
					RegisterPlayerEvent("SetMoveRightTrue");
				break;
				case KeyboardKey.Left:
					RegisterPlayerEvent("SetMoveLeftTrue");
				break;
				default:
				break;
			}
        }
        ///<summary> Sends a message to RegisterPlayerEvent to move player </summary>
		/// <param> Takes a key as a parameter</param>
		/// <output> Nothing</output> 
		public void KeyRelease(KeyboardKey key) {
			switch (key) {
				case KeyboardKey.Escape:
					GalagaBus.GetBus().RegisterEvent(
						new GameEvent {
							EventType = GameEventType.WindowEvent,
							Message = "CLOSE"
						}
					);
					break;
				case KeyboardKey.Right:
					RegisterPlayerEvent("SetMoveRightFalse");
					break;
				case KeyboardKey.Left:
					RegisterPlayerEvent("SetMoveLeftFalse");
					break;
				case KeyboardKey.Space:
					playerShots.AddEntity(new PlayerShot(new Vec2F(player.GetPosition().X+0.047f,player.GetPosition().Y+0.01f),playerShotImage));
					break;
				default:
				break;
			}
		}
        ///<summary> Method to iterate over player shots. Decides what happens with entities hit by shots. </summary>
		/// <param> Take no parameters</param>
		/// <output> No output</output> 
		private void IterateShots() {
			playerShots.Iterate(shot => {
			shot.Shape.Move();
				if (shot.Shape.Position.Y <= 0.0f && shot.Shape.Position.Y >= 1.0f) {
					shot.DeleteEntity();
				} else {
					squadPicker.Enemies.Iterate(enemy => {
						if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
							shot.DeleteEntity();
							if (enemy.hitpoints == 1) {
								enemy.DeleteEntity();
								AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
								score.AddPoints();
								if (squadPicker.Enemies.CountEntities() == 1) {
									newEnemys();
								}
							} else if (enemy.hitpoints == 2) {
								enemy.Image = new ImageStride(40, enemyStridesRed);
								enemy.decreaseHitpoints();
							} else {
								enemy.decreaseHitpoints();
							}

						}

					});
				}
			});
		}
        ///<summary>Method to generate new set of ememies </summary>
		/// <param> Take no parameters</param>
		/// <output> Returns nothing </output> 
        public void newEnemys(){
			var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
			enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets","Images", "RedMonster.png"));
			squadPicker.CreateEnemies(images, enemyStridesRed);
			moveStratPicker.IncreaseSpeed(0.001f);
		}

		///<summary> Method to decide if the game is over or not  </summary>
		/// <param> Take no parameters </param>
		/// <output> No output </output> 
		public void gameover(){
			if (squadPicker.Enemies.CountEntities() == 0) {
				player.DeletePlayerEntity();
				gameOver.SetColor(System.Drawing.Color.Orange);
				gameOver.SetText("Game Over");
			}
			var deleteEnemy = false;
			squadPicker.Enemies.Iterate(enemy => {
				if (enemy.Shape.Position.Y <= 0.2f && deleteEnemy == false) {
					deleteEnemy = true;
				}
			});
			if (deleteEnemy == true) {
				squadPicker.ClearEnemies();
			}
		}
        ///<summary> Method to create an explosion </summary>
		/// <param> A position vector and an extent vector</param>
		/// <output> No output</output> 
		public void AddExplosion(Vec2F position, Vec2F extent) {
			StationaryShape explosion = new StationaryShape(position, extent);
			enemyExplosions.AddAnimation(explosion, EXPLOSION_LENGTH_MS, new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides)); //explosionStrides / 8
		}
        ///<summary> Method to pick a random squadron </summary>
		/// <param> Take no parameters</param>
		/// <output> No output</output> 
		private void SquadPick() {
			switch(new Random().Next(3)) {
				case 0:
					squadPicker = new SquadronStraigtLine(6);
					break;
				case 1:
					squadPicker = new SquadronZigZag(6);
					break;
				case 2:
					squadPicker = new SquadronThreeRows(6);
					break;
				default:
					squadPicker = new SquadronStraigtLine(6);
					break;
			}
		}
            ///<summary> Picks a random moving strategy for the enemy colums </summary>
			/// <param> Take no parameters</param>
			/// <output> No output</output> 
		private void MovePick() {
            switch(new Random().Next(3)) {
                case 0:
                    moveStratPicker = new NoMove();
                    break;
                case 1:
                    moveStratPicker = new Down();
                    break;
                case 2:
                    moveStratPicker = new ZigZagDown();
                    break;
                default:
                    moveStratPicker = new Down();
                    break;
            }
        }
        ///<summary> It register an event and sends it to the Player </summary>
		/// <param> A string as msg</param>
		/// <output> Nothing</output> 
		private void RegisterPlayerEvent(string msg) {
			GalagaBus.GetBus().RegisterEvent (
				new GameEvent {
					EventType = GameEventType.InputEvent,
					From = this,
					To = player,
					Message = msg
				}
			);
		}
	}
}