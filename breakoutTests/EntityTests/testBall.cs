using NUnit.Framework;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Physics;

namespace breakoutTests {
    [TestFixture]

    public class BallTests
    {
        private Ball ball;
        
        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();
            ball = new Ball(
                new Vec2F(0.4f+0.083f,0.05f+0.03f),
                new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Breakout","Assets", "Images", "Ball.png")))
            );
        }
        [Test]
        public void TestUpCollision()
        {
            var initialY = ball.Shape.AsDynamicShape().Direction.Y;
            ball.updateDirection(CollisionDirection.CollisionDirUp);
            Assert.AreEqual(initialY*-1f, ball.Shape.AsDynamicShape().Direction.Y);
        }
        [Test]
        public void TestDownCollision()
        {
            var initialY = ball.Shape.AsDynamicShape().Direction.Y;
            ball.updateDirection(CollisionDirection.CollisionDirDown);
            Assert.AreEqual(initialY*-1f, ball.Shape.AsDynamicShape().Direction.Y);
        }
        [Test]
        public void TestRightCollision()
        {
            var initialX = ball.Shape.AsDynamicShape().Direction.X;
            ball.updateDirection(CollisionDirection.CollisionDirRight);
            Assert.AreEqual(initialX*-1f, ball.Shape.AsDynamicShape().Direction.X);
        }
        [Test]
        public void TestLeftCollision()
        {
            var initialX = ball.Shape.AsDynamicShape().Direction.X;
            ball.updateDirection(CollisionDirection.CollisionDirLeft);
            Assert.AreEqual(initialX*-1f, ball.Shape.AsDynamicShape().Direction.X);
        }
    }
}