using Galaga.MovementStrategy;
using DIKUArcade.Entities;
using System;
namespace Galaga {
    public class ZigZagDown : IMovementStrategy {
        public float Speed = 0.0003f;
        float p = 0.030f;
        
        float a = 0.005f;
        public ZigZagDown() {}

        ///<summary> Moves the enemy  </summary>
        /// <param> Takes a enemy as a parameter</param>
        /// <output> Nothing</output> 
        public void MoveEnemy (Enemy enemy) {
            enemy.Shape.Position.Y = enemy.Shape.Position.Y - Speed;
            enemy.Shape.Position.X = (float)(enemy.Shape.Position.X + a * Math.Sin((2.0*Math.PI*(0.0f-enemy.Shape.Position.Y))/p));
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