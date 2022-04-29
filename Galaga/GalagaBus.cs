using DIKUArcade.Events;
namespace Galaga {
    public static class GalagaBus {
        private static GameEventBus eventBus = default!;
        public static GameEventBus GetBus() {
            return GalagaBus.eventBus ?? (GalagaBus.eventBus = new GameEventBus());
        }
    }
}