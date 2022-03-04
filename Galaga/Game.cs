using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

using System.Security.Principal;
using System.Collections.Generic;
using DIKUArcade.Events;

using DIKUArcade.Physics;

namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {
        private Player player;
        private GameEventBus eventBus;
        private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));


            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            playerShots = new EntityContainer<PlayerShot>();
            for (int i = 0; i < numEnemies; i++) {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, images)));
            }
            
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
        
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);

            enemyExplosions = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
        }
        // TODO: Set key event handler (inherited window field of DIKUGame class)

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                KeyPress(key);
            } else if (action == KeyboardAction.KeyRelease) {
                KeyRelease(key);
            }
        }
        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Right:
                    player.SetMoveRight(true);
                break;
                case KeyboardKey.Left:
                    player.SetMoveLeft(true);
                break;
                default:
                break;
            } 
        }   
        public void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Escape:
                    window.CloseWindow();
                    break;
                case KeyboardKey.Right:
                    player.SetMoveRight(false);
                    break;
                case KeyboardKey.Left:
                    player.SetMoveLeft(false);
                    break;
                case KeyboardKey.Space:
                    playerShots.AddEntity(new PlayerShot(player.GetPosition(),playerShotImage));
                    break;
                default:
                break;
            }
        }


        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) { 
                    default:
                    break;
                } 
            }
        }

        public override void Render() {
            player.Render();
            enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
        }

        public override void Update() {
            window.PollEvents();
            eventBus.ProcessEventsSequentially();
            player.move();
            IterateShots();
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
            shot.Shape.Move();
                if (shot.Shape.Position.Y <= 0.0f && shot.Shape.Position.Y >= 1.0f) {
                    shot.DeleteEntity();
                } else {
                    enemies.Iterate(enemy => {
                        // TODO: if collision btw shot and enemy -> delete both
                        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
                            shot.DeleteEntity();
                            AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                            enemy.DeleteEntity();
                        }
                    });
                }
            });
        }

        public void AddExplosion(Vec2F position, Vec2F extent) {
            // TODO: add explosion to the AnimationContainer
            StationaryShape explosion = new StationaryShape(position, extent);
            enemyExplosions.AddAnimation(explosion, EXPLOSION_LENGTH_MS, new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides)); //explosionStrides / 8
        }
    }
}