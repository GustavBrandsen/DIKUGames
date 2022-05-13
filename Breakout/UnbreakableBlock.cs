using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout {
    public class UnbreakableBlock : Block {
        private IBaseImage image;
        private DynamicShape shape;
        public UnbreakableBlock(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.image = image;
            this.shape = shape;
        }
        ///<summary> Decrements the health field </summary>
        /// <param> Takes no parameter </param>
        /// <output> Nothing </output> 
        public override void decreasehealth () {
            
        }
        public override int getHealth() {
            return 99999;
        }
        public override int getValue() {
            return 0;
        }
    }
}