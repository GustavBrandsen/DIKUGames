using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Galaga {
    public class Enemy : Entity {
        public int hitpoints {get; private set;}
        public IBaseImage image;
        public DynamicShape shape;
        public Enemy(DynamicShape shape, IBaseImage image)
            : base(shape, image) {
                hitpoints = 3;
                this.Image = image;
                this.shape = shape;
            }
        ///<summary> Decrements the hitpoints field </summary>
        /// <param> Takes no parameter </param>
        /// <output> Nothing </output> 
        public void decreaseHitpoints () {
            hitpoints--;
        }
    }
}