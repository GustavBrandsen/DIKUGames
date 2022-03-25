using Galaga.Squadron;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;
namespace Galaga {
    public class SquadronStraigtLine : ISquadron {
        public EntityContainer<Enemy> Enemies {get; set;}
        public int MaxEnemies {get; set;}

        /// <summary> Updates int Maxenemies </summary>
        /// <param> Takes an int </param>
        /// <output> Updated state of MaxEnemies </output> 
        public SquadronStraigtLine(int MaxEnemies) {
            this.MaxEnemies = MaxEnemies;
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
        }
        /// <summary> Method to create new enemies.</summary>
        /// <param> Takes two lists of images </param>
        /// <output> Returns nothing </output> 
        public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride){
            for (int i = 0; i < MaxEnemies; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride)));
            }
        }
        /// <summary> Method delete all enemies in a container.</summary>
        /// <param> Takes no parameters </param>
        /// <output> Returns nothing </output> 
        public void ClearEnemies () {
            this.Enemies.Iterate(enemy => {
                enemy.DeleteEntity();
            });
        }
    }
}