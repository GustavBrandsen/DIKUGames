using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;

namespace Galaga {
    public class Player {
        private Entity entity;
        private DynamicShape shape;

        private float moveLeft = 0.0f;

        private float moveRight = 0.0f; 

        private const float MOVEMENT_SPEED = 0.1f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }
        public void Render() {
            entity.RenderEntity();
        }


        public void move()
        {
            if (shape.Position.X <= 0) {
                shape.Position.X = 0;
            }
            if (shape.Position.X >= 0.9f) {
                shape.Position.X = 0.9f;
            }
            if (shape.Position.X >= 0 && shape.Position.X <=0.9f) {
                shape.Move();
            }
        }

        
        public void SetMoveLeft(bool val)
        {
            if (val == true) {
            moveLeft = moveLeft - MOVEMENT_SPEED;
            }
            else {
                moveLeft = 0.0f; 
            }
            UpdateDirection();
        }

        public void SetMoveRight(bool val)
        {
            if (val == true) {
                moveRight = moveRight + MOVEMENT_SPEED;
            }
            else {
                moveRight = 0.0f;
            }
            UpdateDirection();
        }
        private void UpdateDirection()
        {
            shape.Direction.X = moveLeft + moveRight;

        }
        public Vec2F GetPosition() {
            return shape.Position;
        }
    }
}