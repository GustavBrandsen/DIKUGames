using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout {
    public class HardenedBlock : Block {
        private int health;
        private int value;
        private IBaseImage image;
        private DynamicShape shape;
        private bool powerUp = false;
        public HardenedBlock(DynamicShape shape, IBaseImage image, bool PowerUp) : base(shape, image) {
            this.health = 2;
            this.value = 2;
            this.powerUp = PowerUp;
            this.image = image;
            this.shape = shape;
        }
        
        ///<summary> Decrements the health field </summary>
        /// <param> Takes no parameter </param>
        /// <output> Nothing </output> 
        public override void Decreasehealth() {
            health--;
            if (health == 1) {
                
            }
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