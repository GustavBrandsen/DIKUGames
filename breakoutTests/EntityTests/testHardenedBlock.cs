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
        private HardenedBlock Hardenedblock;
        private DefaultBlock block;
        
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
            Assert.IsNotNull(Hardenedblock.getHealth());
        }
        [Test]
        public void TestValueExists()
        {
            Assert.IsNotNull(Hardenedblock.getValue());
        }
        [Test]
        public void TestBlockHit()
        {
            Hardenedblock.decreasehealth();
            Assert.AreEqual(Hardenedblock.getHealth(), 1);
        }
        [Test]
        public void TestBlockDoubleHP()
        {
            Assert.AreEqual(Hardenedblock.getHealth(), block.getHealth()*2);
        }
        [Test]
        public void TestBlockDoubleValue()
        {
            Assert.AreEqual(Hardenedblock.getValue(), block.getValue()*2);
        }
    }
}