using Galaga.MovementStrategy;
using DIKUArcade.Entities;
namespace Galaga {
    public class NoMove : IMovementStrategy {
        public NoMove() {}
        public void MoveEnemy (Enemy enemy) {

        }
        public void MoveEnemies (EntityContainer<Enemy> enemies) {

        }
        public void IncreaseSpeed (float increment){}
    }
}