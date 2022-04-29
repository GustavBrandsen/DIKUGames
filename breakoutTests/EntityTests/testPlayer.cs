using NUnit.Framework;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Threading;

namespace breakoutTests {
    [TestFixture]

    public class PlayerTests
    {
        private Player player;
        
        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","Assets", "Images", "player.png"))));
                
            //BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });    
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, player);
			
            
            
        }
        [Test]
        public void TestInitialPosition()
        {
            Assert.AreEqual(Math.Round(player.GetPosition().X), Math.Round(0.5f - 0.5*player.GetExtent().X));
        }
        [Test]
        public void TestMoveRight()
        {
            System.Console.WriteLine(player.GetPosition().X);
            //BreakoutBus.GetBus().RegisterEvent (new GameEvent {EventType = GameEventType.InputEvent, From = this, To = player, Message = "SetMoveRightTrue"});
            //SendPlayerInput("SetMoveRightTrue");
            BreakoutBus.GetBus().ProcessEventsSequentially();
            System.Console.WriteLine(player.GetPosition().X);
            //Assert.Less((0.5f - 0.5*player.GetExtent().X), (player.GetPosition().X));
            player.movePlayer();
            Assert.AreEqual((player.GetPosition().X), (0.5f - 0.5*player.GetExtent().X));
        }

        [Test]
        public void TestMoveLeft(){

            System.Console.WriteLine(player.GetPosition().X);
            BreakoutBus.GetBus().RegisterEvent (new GameEvent {EventType = GameEventType.InputEvent, From = this, Message = "SetMoveLeftTrue"});
            System.Console.WriteLine(player.GetPosition().X);
            Assert.Less(0.0f, player.moveLeft);

        }
    }
}