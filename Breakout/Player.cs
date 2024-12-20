using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout {
    public class Player : Entity, IGameEventProcessor, IPlayer {
        private IBaseImage image;
        private DynamicShape shape;
        public float moveLeft {get; private set;} = 0.0f;
        public float moveRight {get; private set;}= 0.0f;
        private float MOVEMENT_SPEED = 0.02f;
        public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.image = image;
            this.shape = shape;
			BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        }
        
        public void DoubleSpeed() {
            MOVEMENT_SPEED = 0.04f;
        }

        public void NormalSpeed() {
            MOVEMENT_SPEED = 0.02f;
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

        ///<summary>
        /// Sets the Movement methods to true depending on player events from GameRunning
        ///</summary>
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
        /// <summary> Moves the player within the bondaries of the game</summary>
        public void MovePlayer() {
            if (!(moveLeft == 0.0f)) {
                if (shape.Position.X > 0 && shape.Position.X <= 0.98f-shape.Extent.X + MOVEMENT_SPEED) {
                    shape.Move();
                }
            }
            if (!(moveRight == 0.0f)) {
                if (shape.Position.X >= 0f - MOVEMENT_SPEED && shape.Position.X <= 0.98f-shape.Extent.X) {
                    shape.Move();
                }
            }
        }
        /// <summary> Set the field moveLeft to the correct number </summary>
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
        /// <summary> Set the field moveRight to the correct number</summary>
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

        /// <summary> Double the players extend </summary>
        public void DoubleSize() {
            this.Shape.Extent.X =  this.Shape.Extent.X+0.2f;
            if (this.Shape.Position.X+this.Shape.Extent.X >= 0.98f) {
                this.Shape.Position.X = 1.0f-this.Shape.Extent.X;
            } else {
                this.Shape.Position.X = this.Shape.Position.X-0.1f;
            }
        }
        /// <summary> Halfs the players extent</summary>
        public void NormalSize() {
            this.Shape.Extent.X =  this.Shape.Extent.X-0.2f;
            this.Shape.Position.X = this.Shape.Position.X+0.1f;
        }
    }
}