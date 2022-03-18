using Galaga.MovementStrategy;
using DIKUArcade.Entities;
namespace Galaga {
    public class Down : IMovementStrategy {
        public float Speed = 0.001f;
        public Down() {}
            
        ///<summary> Moves the enemy  </summary>
        /// <param> Takes a enemy as a parameter</param>
        /// <output> Nothing</output>
        public void MoveEnemy (Enemy enemy) {
            enemy.Shape.MoveY(-Speed);
        }
        ///<summary> Moves the enemys within the enemy container </summary>
        /// <param> Takes an enemy container as a paramter</param>
        /// <output> Nothing</output> 
        public void MoveEnemies (EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy => {
                MoveEnemy(enemy);
            });
        }
        ///<summary> Increases the speed field </summary>
        /// <param> Takes a float value</param>
        /// <output> Nothing</output> 
        public void IncreaseSpeed (float increment){
            Speed = Speed+increment;
        }
    }
}