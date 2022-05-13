using Breakout;
using Breakout.BreakoutStates;
using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using System.Collections.Generic;

namespace BreakoutTests {
    [TestFixture]
    public class StateMachineTesting {
        private StateMachine stateMachine = new StateMachine();
        private GameEventBus gamebus;
        [SetUp]
        public void InitiateStateMachine() {
            Window.CreateOpenGLContext();
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.GameStateEvent});
            gamebus = BreakoutBus.GetBus();
            
            gamebus.Subscribe(GameEventType.GameStateEvent, stateMachine);
        }
        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }
        [Test]
        public void TestEventGamePaused() {
            gamebus.RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                }
            );
            gamebus.ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>()); 
        }
        [Test]
        public void TestEventGameRunning() {
            gamebus.RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING"
                }
            );
            gamebus.ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>()); 
        }
        [Test]
        public void TestEventMainMenu() {
            gamebus.RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                }
            );
            gamebus.ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>()); 
        }
    }
}