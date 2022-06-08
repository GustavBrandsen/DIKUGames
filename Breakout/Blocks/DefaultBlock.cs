using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout {
    public class DefaultBlock : Block {
        private int health;
        private int value;
        private IBaseImage image;
        private DynamicShape shape;
        private bool powerUp = false;
        public DefaultBlock(DynamicShape shape, IBaseImage image, bool PowerUp) : base(shape, image) {
            this.health = 1;
            this.value = 1;
            this.powerUp = PowerUp;
            this.image = image;
            this.shape = shape;
        }
        
        ///<summary> Decrements the health field </summary>
        public override void Decreasehealth () {
            health--;
            if (health == 0) {
                this.DeleteEntity();
            }
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