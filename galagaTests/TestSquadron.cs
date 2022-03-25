using NUnit.Framework;
using Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;

namespace galagaTests;

public class TestSquadron
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestSquadronStraightLineInstance ()
    { 
        var straightLine = new SquadronStraigtLine(6);
        Assert.AreEqual(6, straightLine.MaxEnemies);
    }
    [Test]
    public void TestSquadronStraightLineClear ()
    { 
        var images = ImageStride.CreateStrides(4, Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "BlueMonster.png")));
		var enemyStridesRed = ImageStride.CreateStrides(2, Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "RedMonster.png")));

        var straightLine = new SquadronStraigtLine(6);
        straightLine.CreateEnemies(images, enemyStridesRed);
        var enemies = straightLine.Enemies.CountEntities();
        straightLine.ClearEnemies();
        Assert.AreNotEqual(enemies, straightLine.Enemies.CountEntities());
    }
    [Test]
    public void TestSquadronThreeRowsInstance ()
    { 
        var threeRows = new SquadronStraigtLine(6);
        Assert.AreEqual(6, threeRows.MaxEnemies);
    }
    [Test]
    public void TestSquadronThreeRowsClear ()
    { 
        var images = ImageStride.CreateStrides(4, Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "BlueMonster.png")));
		var enemyStridesRed = ImageStride.CreateStrides(2, Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "RedMonster.png")));

        var threeRows = new SquadronStraigtLine(6);
        threeRows.CreateEnemies(images, enemyStridesRed);
        var enemies = threeRows.Enemies.CountEntities();
        threeRows.ClearEnemies();
        Assert.AreNotEqual(enemies, threeRows.Enemies.CountEntities());
    }
    [Test]
    public void TestSquadronZigZagInstance ()
    { 
        var zigZag = new SquadronStraigtLine(6);
        Assert.AreEqual(6, zigZag.MaxEnemies);
    }
    [Test]
    public void TestSquadronZigZagClear ()
    { 
        var images = ImageStride.CreateStrides(4, Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "BlueMonster.png")));
		var enemyStridesRed = ImageStride.CreateStrides(2, Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "..","..","..","..","Galaga","Assets", "Images", "RedMonster.png")));

        var zigZag = new SquadronStraigtLine(6);
        zigZag.CreateEnemies(images, enemyStridesRed);
        var enemies = zigZag.Enemies.CountEntities();
        zigZag.ClearEnemies();
        Assert.AreNotEqual(enemies, zigZag.Enemies.CountEntities());
    }

  
}