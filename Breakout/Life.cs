using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
namespace Breakout {
    public class Life : Entity{
        private IBaseImage image;
        private DynamicShape shape;
        public Life (DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            this.image = image;
        }
        /// <summary> Render the life to the screen </summary>
        public void Render() {
            this.RenderEntity();
        }
    }
}