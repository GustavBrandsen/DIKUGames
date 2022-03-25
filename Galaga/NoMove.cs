using Galaga.MovementStrategy;
using DIKUArcade.Entities;
namespace Galaga {
    public class NoMove : IMovementStrategy {
        public float Speed = 0.0f;
        public NoMove() {}
        public void MoveEnemy (Enemy enemy) {

        }
        public void MoveEnemies (EntityContainer<Enemy> enemies) {

        }
        public void IncreaseSpeed (float increment){}
    }
}