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
        private Player player = default!;
        
        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","Assets", "Images", "player.png"))));
        }
        
        [Test]
        public void TestInitialPosition()
        {
            Assert.AreEqual(Math.Round(player.GetPosition().X), Math.Round(0.5f - 0.5*player.GetExtent().X));
        }
        [Test]
        public void TestMoveRight()
        {
            player.ProcessEvent(
                new GameEvent {
                    EventType = GameEventType.InputEvent,
                    To = player,
                    Message = "MoveRightTrue"
                }
            );
            player.MovePlayer();
            Assert.AreNotEqual(0.5f - 0.5*player.GetExtent().X, player.GetPosition().X);
        }

        [Test]
        public void TestMoveLeft(){
            player.ProcessEvent(
                new GameEvent {
                    EventType = GameEventType.InputEvent,
                    To = player,
                    Message = "MoveLeftTrue"
                }
            );
            player.MovePlayer();
            Assert.AreNotEqual(player.GetPosition().X, 0.5f - 0.5*player.GetExtent().X);
        }
    }
}