using NUnit.Framework;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;
using System.IO;
using DIKUArcade.Entities;

namespace galagaTests;

public class TestEnemy
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestEnemyHP ()
    {
        var randInt = new Random().Next(3);
        var image = new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "BlueMonster.png")));
        var shape = new DynamicShape(new Vec2F(0.0f,0.5f), new Vec2F(0.5f,0.5f));
        var enemy = new Enemy(shape, image);
        for (int i = 0; i < randInt; i++) {
            enemy.decreaseHitpoints();
        }
        Assert.AreEqual(3-randInt, enemy.hitpoints);
    }

  
}