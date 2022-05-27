using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System;
namespace Breakout {
    public class ExtraPlayerSpeed : PowerUp {
        private static Vec2F extent = new Vec2F(0.04f, 0.04f);
        private Vec2F direction = new Vec2F(0.00f, -0.005f);
        public ExtraPlayerSpeed(Vec2F pos) : base(new DynamicShape(pos, extent), new Image(Path.Combine("Assets", "Images", "DoubleSpeedPowerUp.png"))) {
            this.Shape.AsDynamicShape().Direction = direction;
        }
    }
}