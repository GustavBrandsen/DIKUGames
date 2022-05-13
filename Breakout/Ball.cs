using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System;
namespace Breakout {
    public class Ball : Entity {
        private float speed = 0.04f;
        private float randomX = new Random().Next(-3, 3)/100f;
        private static Vec2F extent = new Vec2F(0.04f, 0.04f);
        private Vec2F direction;
        public Ball(Vec2F vec, IBaseImage image) : base(new DynamicShape(vec, extent), image) {
            direction = new Vec2F(randomX, speed-Math.Abs(randomX));
            this.Shape.AsDynamicShape().ChangeDirection(direction);
        }
        public void updateDirection(CollisionDirection dir){
            if (this.Shape.AsDynamicShape().Direction.X == 0f) {
                this.Shape.AsDynamicShape().ChangeDirection(new Vec2F(this.Shape.AsDynamicShape().Direction.X, this.Shape.AsDynamicShape().Direction.Y));
            }
            switch (dir) {
            case CollisionDirection.CollisionDirUp:
                this.Shape.AsDynamicShape().ChangeDirection(new Vec2F(this.Shape.AsDynamicShape().Direction.X, this.Shape.AsDynamicShape().Direction.Y*-1f));
                break;
            case CollisionDirection.CollisionDirDown:
                this.Shape.AsDynamicShape().ChangeDirection(new Vec2F(this.Shape.AsDynamicShape().Direction.X, this.Shape.AsDynamicShape().Direction.Y*(-1f)));
                break;
            case CollisionDirection.CollisionDirLeft:
                this.Shape.AsDynamicShape().ChangeDirection(new Vec2F(this.Shape.AsDynamicShape().Direction.X*-1f, this.Shape.AsDynamicShape().Direction.Y));
                break;
            case CollisionDirection.CollisionDirRight:
                this.Shape.AsDynamicShape().ChangeDirection(new Vec2F(this.Shape.AsDynamicShape().Direction.X*-1f, this.Shape.AsDynamicShape().Direction.Y));
                break;
            }
        }
    }
}