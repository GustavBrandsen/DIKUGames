using DIKUArcade.Math;
using DIKUArcade.Events;
namespace Breakout {
    public interface IPlayer {
        void Render();
        Vec2F GetPosition();
        Vec2F GetExtent();
        void ProcessEvent(GameEvent gameEvent);
        void MovePlayer();
    }
}
