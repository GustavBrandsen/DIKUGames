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

public class TestPoint
{
    private Points point = default!;
    [SetUp]
    public void Setup()
    {
        Window.CreateOpenGLContext();
        point = new Points(new Vec2F(0.0f,0.5f), new Vec2F(0.5f,0.5f));
    }

    [Test]
    public void TestpointAdd ()
    {
        for (int i = 0; i < 100; i++) {
            point.AddPoints(1);
        }
        Assert.AreEqual(100, point.GetPoints());
    }

  
}