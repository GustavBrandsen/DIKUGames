using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Galaga {
    public class Enemy : Entity {
        public int hitpoints {get; private set;}
        public IBaseImage image;
        public Enemy(DynamicShape shape, IBaseImage image)
            : base(shape, image) {
                hitpoints = 3;
            }
        ///<summary> Decrements the hitpoints field </summary>
        /// <param> Takes no parameter </param>
        /// <output> Nothing </output> 
        public void decreaseHitpoints () {
            hitpoints--;
        }
    }
}