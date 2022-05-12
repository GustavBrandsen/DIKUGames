using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout {
    public class Block : Entity {
        private int health;
        private int value;
        private IBaseImage image;
        private DynamicShape shape;
        public Block(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.health = 1;
            this.value = 3;
            this.image = image;
            this.shape = shape;
        }
        ///<summary> Decrements the health field </summary>
        /// <param> Takes no parameter </param>
        /// <output> Nothing </output> 
        public void decreasehealth () {
            health--;
            if (health == 0) {
                this.DeleteEntity();
            }
        }
        public int getHealth() {
            return health;
        }
        public int getValue() {
            return value;
        }
    }
}