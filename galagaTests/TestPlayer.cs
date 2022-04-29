using NUnit.Framework;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using System.Collections.Generic;

namespace galagaTests {
    [TestFixture]
    public class TestPlayer{
        private Player player;
        private DynamicShape playerShape;
        private GameEventBus gamebus;
        [OneTimeSetUp]
        public void Setup(){
            playerShape = new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f));
            player = new Player(
                    new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                    new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "Player.png"))));
            
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent});
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, player);
        }

        [Test]
        public void TestPlayerMoveLeft () {
            GalagaBus.GetBus().RegisterEvent (new GameEvent {EventType = GameEventType.InputEvent, From = this, Message = "SetMoveLeftTrue"});
            GalagaBus.GetBus().ProcessEventsSequentially();
            player.move();
            Assert.Less(playerShape.Position, player.GetPosition());
        }

    
    }
}

