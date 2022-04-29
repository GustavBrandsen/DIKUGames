using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout {
    public class Block : Entity {
        public int health {get; private set;}
        public int value {get; private set;}
        public IBaseImage image;
        public DynamicShape shape;
        public Block(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.health = 3;
            this.value = 3;
            this.image = image;
            this.shape = shape;
        }
        ///<summary> Decrements the health field </summary>
        /// <param> Takes no parameter </param>
        /// <output> Nothing </output> 
        public void decreasehealth () {
            health--;
        }
    }
}