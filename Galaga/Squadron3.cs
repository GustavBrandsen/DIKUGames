using Galaga.Squadron;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;
namespace Galaga {
    public class Squadron3 : ISquadron {
        public EntityContainer<Enemy> Enemies {get; set;}
        public int MaxEnemies {get; set;}

        /// <summary> Updates int Maxenemies </summary>
        /// <param> Takes an int </param>
        /// <output> Updated state of MaxEnemies </output> 
        public Squadron3(int MaxEnemies) {
            this.MaxEnemies = MaxEnemies*3;
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
        }
        /// <summary> Method to create new enemies.</summary>
        /// <param> Takes two lists of images </param>
        /// <output> Returns nothing </output> 
        public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride){
            for (int i = 0; i < MaxEnemies; i++) {
                float x = (float) i;
                float y;
                if (i < MaxEnemies / 3) {
                    y = 0.0f;
                }
                else if (i >= MaxEnemies / 3 && i < (MaxEnemies / 3) * 2) {
                    y = 0.1f;
                    x = i - MaxEnemies / 3;
                } else {
                    y = 0.2f;
                    x = i - (MaxEnemies / 3)*2;
                }
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + x * 0.1f, 0.9f - y), new Vec2F(0.1f, 0.1f)),
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