using NUnit.Framework;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;
using System.IO;
using DIKUArcade.Entities;

namespace galagaTests;

public class TestMovementStrategy
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestNoMoveIncreaseSpeed ()
    {
        var noMove = new NoMove();
        noMove.IncreaseSpeed(999);
        Assert.AreEqual(0, noMove.Speed);
    }
    [Test]
    public void TestNoMoveMoveEnemy ()
    {
        var noMove = new NoMove();
        var image = new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "BlueMonster.png")));
        var shape = new DynamicShape(new Vec2F(0.0f,0.5f), new Vec2F(0.5f,0.5f));
        var enemy = new Enemy(shape, image);
        var enemyStartPos = enemy.Shape.Position;
        noMove.MoveEnemy(enemy);
        Assert.AreEqual(enemyStartPos, enemy.Shape.Position);
    }

    [Test]
    public void TestDownIncreaseSpeed ()
    {
        var down = new Down();
        down.IncreaseSpeed(999.0f);
        Assert.AreEqual(999.0f, Math.Round(down.Speed));
    }
    [Test]
    public void TestDownMoveEnemy ()
    {
        var down = new Down();
        var image = new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "BlueMonster.png")));
        var shape = new DynamicShape(new Vec2F(0.0f,0.5f), new Vec2F(0.5f,0.5f));
        var enemy = new Enemy(shape, image);
        var enemyStartPos = new Vec2F(0.0f,0.5f);
        down.MoveEnemy(enemy);
        Assert.AreNotEqual(enemyStartPos, enemy.Shape.Position);
    }
    [Test]
    public void TestZigZagIncreaseSpeed ()
    {
        var zigZag = new ZigZagDown();
        zigZag.IncreaseSpeed(999.0f);
        Assert.AreEqual(999.0f, Math.Round(zigZag.Speed));
    }
    [Test]
    public void TestZigZagMoveEnemy ()
    {
        var zigZag = new ZigZagDown();
        var image = new Image(Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "BlueMonster.png")));
        var shape = new DynamicShape(new Vec2F(0.0f,0.5f), new Vec2F(0.5f,0.5f));
        var enemy = new Enemy(shape, image);
        var enemyStartPos = new Vec2F(0.0f,0.5f);
        zigZag.MoveEnemy(enemy);
        Assert.AreNotEqual(enemyStartPos, enemy.Shape.Position);
    }

}