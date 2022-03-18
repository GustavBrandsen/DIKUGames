using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.Security.Principal;
using System.Collections.Generic;

namespace Galaga {
    public class Player : IGameEventProcessor {
        private Entity entity;
        private DynamicShape shape;

        private float moveLeft = 0.0f;

        private float moveRight = 0.0f; 

        private const float MOVEMENT_SPEED = 0.01f;
        /// <summary> Objects contructor.</summary>
        /// <param> Takes a DynamicShape and an IbaseImage </param>
        /// <output> A new player object </output> 
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }
        /// <summary> Method to render a player </summary>
        /// <param> Takes no parameters </param>
        /// <output> Returns nothing </output> 
        public void Render() {
            entity.RenderEntity();
        }
        /// <summary> Method to move a player.</summary>
        /// <param> Takes no parameters </param>
        /// <output> Returns nothing </output> 
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
        /// <summary> Private bool to check if a player can move left</summary>
        /// <param> a bool </param>
        /// <output> Returns nothing </output> 
        private void SetMoveLeft(bool val)
        {
            if (val == true) {
            moveLeft = moveLeft - MOVEMENT_SPEED;
            }
            else {
                moveLeft = 0.0f; 
            }
            UpdateDirection();
        }
        /// <summary> Private bool to check if a player can move right</summary>
        /// <param> a bool </param>
        /// <output> Returns nothing </output> 
        private void SetMoveRight(bool val)
        {
            if (val == true) {
                moveRight = moveRight + MOVEMENT_SPEED;
            }
            else {
                moveRight = 0.0f;
            }
            UpdateDirection();
        }
        /// <summary> Method to update direction of a player</summary>
        /// <param> Takes no parameters </param>
        /// <output> Returns nothing </output> 
        private void UpdateDirection()
        {
            shape.Direction.X = moveLeft + moveRight;

        }
        /// <summary> Method to get the position of the player </summary>
        /// <param> Takes no parameters </param>
        /// <output> Returns nothing </output> 
        public Vec2F GetPosition() {
            return shape.Position;
        }
        /// <summary> Method to procces game events </summary>
        /// <param> Takes a GameEvent </param>
        /// <output> Returns nothing </output> 
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.InputEvent) {
                switch (gameEvent.Message) { 
                    case "SetMoveLeftTrue":
                        SetMoveLeft(true);
                        break;
                    case "SetMoveRightTrue":
                        SetMoveRight(true);
                        break;
                    case "SetMoveLeftFalse":
                        SetMoveLeft(false);
                        break;
                    case "SetMoveRightFalse":
                        SetMoveRight(false);
                        break;
                    default:
                    break;
                } 
            }
        }
        /// <summary> Method to delete a player </summary>
        /// <param> Takes no parameters </param>
        /// <output> Returns nothing </output> 
        public void DeletePlayerEntity(){
            this.shape.MoveY((float)1000000000);
        }
    }
}