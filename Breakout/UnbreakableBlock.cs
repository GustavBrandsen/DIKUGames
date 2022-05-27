using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout {
    public class UnbreakableBlock : Block {
        private IBaseImage image;
        private DynamicShape shape;
        private int health;
        private int value;
        private bool powerUp = false;
        public UnbreakableBlock(DynamicShape shape, IBaseImage image, bool PowerUp) : base(shape, image) {
            this.health = 99999;
            this.value = 0;
            this.powerUp = PowerUp;
            this.image = image;
            this.shape = shape;
        }
        ///<summary> Decrements the health field </summary>
        /// <param> Takes no parameter </param>
        /// <output> Nothing </output> 
        public override void Decreasehealth () {
            
        }
        public override int GetHealth() {
            return health;
        }
        public override int GetValue() {
            return value;
        }
        public override bool CheckPowerUp() {
            return powerUp;
        }
    }
}