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

namespace BreakoutTests;

public class TestScore
{
    private Score score;
    [SetUp]
    public void Setup()
    {
        Window.CreateOpenGLContext();
        score = new Score(new Vec2F(0.0f,0.5f), new Vec2F(0.5f,0.5f));
    }

    [Test]
    public void TestScoreAdd ()
    {
        for (int i = 0; i < 100; i++) {
            score.AddPoints(1);
        }
        Assert.AreEqual(100, score.GetScore());
    }

  
}