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
        private Ball ball = default!;
        
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
            ball.UpdateDirection(CollisionDirection.CollisionDirUp);
            Assert.AreEqual(Math.Round(initialY*-1f), Math.Round(ball.Shape.AsDynamicShape().Direction.Y));
        }
        [Test]
        public void TestDownCollision()
        {
            var initialY = ball.Shape.AsDynamicShape().Direction.Y;
            ball.UpdateDirection(CollisionDirection.CollisionDirDown);
            Assert.AreEqual(Math.Round(initialY*-1f), Math.Round(ball.Shape.AsDynamicShape().Direction.Y));
        }
        [Test]
        public void TestRightCollision()
        {
            var initialX = ball.Shape.AsDynamicShape().Direction.X;
            ball.UpdateDirection(CollisionDirection.CollisionDirRight);
            Assert.AreEqual(Math.Round(initialX*-1f), Math.Round(ball.Shape.AsDynamicShape().Direction.X));
        }
        [Test]
        public void TestLeftCollision()
        {
            var initialX = ball.Shape.AsDynamicShape().Direction.X;
            ball.UpdateDirection(CollisionDirection.CollisionDirLeft);
            Assert.AreEqual(Math.Round(initialX*-1f), Math.Round(ball.Shape.AsDynamicShape().Direction.X));
        }
    }
}