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

    public class HardenedBlockTests
    {
        private HardenedBlock Hardenedblock = default!;
        private DefaultBlock block = default!;
        
        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();
            Hardenedblock = new HardenedBlock(
                new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Breakout","Assets", "Images", "blue-block.png"))));

            block = new DefaultBlock(
                new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.2f, 0.03f)),
                new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Breakout","Assets", "Images", "blue-block.png"))));
        }
        [Test]
        public void TestHealthExists()
        {
            Assert.IsNotNull(Hardenedblock.GetHealth());
        }
        [Test]
        public void TestValueExists()
        {
            Assert.IsNotNull(Hardenedblock.GetValue());
        }
        [Test]
        public void TestBlockHit()
        {
            Hardenedblock.Decreasehealth();
            Assert.AreEqual(Hardenedblock.GetHealth(), 1);
        }
        [Test]
        public void TestBlockDoubleHP()
        {
            Assert.AreEqual(Hardenedblock.GetHealth(), block.GetHealth()*2);
        }
        [Test]
        public void TestBlockDoubleValue()
        {
            Assert.AreEqual(Hardenedblock.GetValue(), block.GetValue()*2);
        }
    }
}