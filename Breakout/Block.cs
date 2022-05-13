using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout {
    public abstract class Block : Entity {
        public Block(DynamicShape shape, IBaseImage image) : base(shape, image) {}
        public abstract void decreasehealth ();
        public abstract int getHealth();
        public abstract int getValue();
    }
}