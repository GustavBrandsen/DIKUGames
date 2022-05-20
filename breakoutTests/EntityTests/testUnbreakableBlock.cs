using NUnit.Framework;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System;
using DIKUArcade.GUI;
using DIKUArcade.Events;

namespace breakoutTests {
    [TestFixture]

    public class UnbreakableBlockTests
    {
        private UnbreakableBlock block = default!;
        
        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();
            block = new UnbreakableBlock(
                new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Breakout","Assets", "Images", "blue-block.png"))));
        }
        [Test]
        public void TestHealthExists()
        {
            Assert.IsNotNull(block.GetHealth());
        }
        [Test]
        public void TestValueExists()
        {
            Assert.IsNotNull(block.GetValue());
        }
        [Test]
        public void TestBlockHit()
        {
            block.Decreasehealth();
            Assert.AreEqual(block.GetHealth(), 0);
        }
    }
}