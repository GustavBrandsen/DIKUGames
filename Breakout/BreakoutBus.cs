using DIKUArcade.Events;
namespace Breakout {
    public static class BreakoutBus {
        private static GameEventBus eventBus = default!;
        /// <summary> Create a new Breakoutbus or return an existing one </summary>
        /// <return> Returns the Breakoutbus </return>
        public static GameEventBus GetBus() {
            return BreakoutBus.eventBus ?? (BreakoutBus.eventBus = new GameEventBus());
        }
    }
}