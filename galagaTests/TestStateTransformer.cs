using NUnit.Framework;

namespace galagaTests;

public class StateTransformer
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GameRunningString()
    {
        Assert.AreEqual(Galaga.StateTransformer.TransformStringToState("GAME_RUNNING"), Galaga.GameStateType.GameRunning);
    }
    [Test]
    public void GamePausedString()
    {
        Assert.AreEqual(Galaga.StateTransformer.TransformStringToState("GAME_PAUSED"), Galaga.GameStateType.GamePaused);
    }
    [Test]
    public void MainMenuString()
    {
        Assert.AreEqual(Galaga.StateTransformer.TransformStringToState("MAIN_MENU"), Galaga.GameStateType.MainMenu);
    }
    
    [Test]
    public void GameRunningState()
    {
        Assert.AreEqual(Galaga.StateTransformer.TransformStateToString(Galaga.GameStateType.GameRunning), "GAME_RUNNING");
    }
    [Test]
    public void MainMenuState()
    {
        Assert.AreEqual(Galaga.StateTransformer.TransformStateToString(Galaga.GameStateType.MainMenu), "MAIN_MENU");
    }
    [Test]
    public void GamePausedState()
    {
        Assert.AreEqual(Galaga.StateTransformer.TransformStateToString(Galaga.GameStateType.GamePaused), "GAME_PAUSED");
    }

  
}