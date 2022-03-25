using NUnit.Framework;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;

namespace galagaTests;

public class TestScore
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestScoreAdd ()
    {
        var randInt = new Random().Next(10);
        var score = new Score(new Vec2F(0.0f,0.5f), new Vec2F(0.5f,0.5f));
        for (int i = 0; i < randInt; i++) {
            score.AddPoints();
        }
        Assert.AreEqual(randInt, score.GetScore());
    }

  
}