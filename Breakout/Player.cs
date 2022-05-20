using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout {
    public class Player : Entity, IGameEventProcessor {
        private IBaseImage image;
        private DynamicShape shape;
        public float moveLeft {get; private set;} = 0.0f;
        public float moveRight {get; private set;}= 0.0f;
        private const float MOVEMENT_SPEED = 0.02f;
        public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.image = image;
            this.shape = shape;
			BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        }
        public void Render() {
            this.RenderEntity();
        }
        public Vec2F GetPosition() {
            return shape.Position;
        }
        public Vec2F GetExtent() {
            return shape.Extent;
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.InputEvent) {
                switch (gameEvent.Message) {
                    case "MoveLeftTrue":
                        MoveLeft(true);
                        break;
                    case "MoveRightTrue":
                        MoveRight(true);
                        break;
                    case "MoveLeftFalse":
                        MoveLeft(false);
                        break;
                    case "MoveRightFalse":
                        MoveRight(false);
                        break;
                    default:
                        break;
                } 
            }
        }
        public void MovePlayer() {
            if (!(moveLeft == 0.0f)) {
                if (shape.Position.X > 0 && shape.Position.X <= 0.78f + MOVEMENT_SPEED) {
                    shape.Move();
                }
            }
            if (!(moveRight == 0.0f)) {
                if (shape.Position.X >= 0f - MOVEMENT_SPEED && shape.Position.X <= 0.78f) {
                    shape.Move();
                }
            }
        }
        private void MoveLeft(bool val) {
            switch (val) {
                case true:
                    moveLeft = moveLeft - MOVEMENT_SPEED;
                    break;
                case false:
                    moveLeft = 0.0f;
                    break;
            }
            NewDirection();
        }
        private void MoveRight(bool val) {
            switch (val) {
                case true:
                    moveRight = moveRight + MOVEMENT_SPEED;
                    break;
                case false:
                    moveRight = 0.0f;
                    break;
            }
            NewDirection();
        }
        private void NewDirection() {
            shape.Direction.X = moveLeft + moveRight;
        }
    }
}