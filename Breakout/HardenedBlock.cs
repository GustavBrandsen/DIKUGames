using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout {
    public class HardenedBlock : Block {
        private int health;
        private int value;
        private IBaseImage image;
        private DynamicShape shape;
        public HardenedBlock(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.health = 2;
            this.value = 2;
            this.image = image;
            this.shape = shape;
        }
        ///<summary> Decrements the health field </summary>
        /// <param> Takes no parameter </param>
        /// <output> Nothing </output> 
        public override void decreasehealth () {
            health--;
            if (health == 1) {
                
            }
            if (health == 0) {
                this.DeleteEntity();
            }
        }
        public override int getHealth() {
            return health;
        }
        public override int getValue() {
            return value;
        }
    }
}