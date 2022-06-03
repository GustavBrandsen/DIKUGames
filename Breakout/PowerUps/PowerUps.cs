using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Breakout {
    public abstract class PowerUp : Entity {
        public PowerUp(DynamicShape shape, IBaseImage image) : base(shape, image) {}

    }
}