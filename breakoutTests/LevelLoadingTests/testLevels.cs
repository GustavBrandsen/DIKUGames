using NUnit.Framework;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using System.Collections.Generic;

namespace breakoutTests {
    [TestFixture]

    public class LevelTests
    {
        private Level level0;
        private Level level1;
        private Level level2;
        
        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();
            level0 = new Level("../../../../../Assets/Levels/empty-level.txt");
            level1 = new Level("../../../../../Assets/Levels/level1.txt");
            level2 = new Level("../../../../../Assets/Levels/level2.txt");
        }
        [Test]
        public void TestDifferencesInLevels()
        {
            Assert.AreNotEqual(level1.legend, level2.legend);
        }
        [Test]
        public void TestDatastructureMap()
        {
            List<string> mapTest = new List<string>{};
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("-aaaaaaaaaa-");
            mapTest.Add("-aaaaaaaaaa-");
            mapTest.Add("-000----000-");
            mapTest.Add("-000-%%-000-");
            mapTest.Add("-000-11-000-");
            mapTest.Add("-000-%%-000-");
            mapTest.Add("-000----000-");
            mapTest.Add("-%%%%%%%%%%-");
            mapTest.Add("-%%%%%%%%%%-");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            mapTest.Add("------------");
            Assert.AreEqual(mapTest, level1.map);
        }
        [Test]
        public void TestDatastructureMeta()
        {
            var metaTest = new Dictionary<string, string>();
            metaTest.Add("Name","LEVEL 1");
            metaTest.Add("Time","300");
            metaTest.Add("Hardened","#");
            metaTest.Add("PowerUp","2");
            
            Assert.AreEqual(metaTest, level1.meta);
        }
        [Test]
        public void TestDatastructureLegend()
        {
            var legendTest = new Dictionary<char, string>();
            legendTest.Add('%',"blue-block.png");
            legendTest.Add('0',"grey-block.png");
            legendTest.Add('1',"orange-block.png");
            legendTest.Add('a',"purple-block.png");
            
            Assert.AreEqual(legendTest, level1.legend);
        }
        [Test]
        public void TestEmptyFile()
        {
            //Assert.DoesNotThrow(level0.Render());
        }
    }
}