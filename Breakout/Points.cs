using DIKUArcade.Math;
using DIKUArcade.Graphics;
namespace Breakout {
    public class Points : IPoints {
        private int points;
        private Text display;
        public Points (Vec2F position, Vec2F extent) {
            points = 0;
            display = new Text (points.ToString(), position, extent);
            display.SetColor(System.Drawing.Color.White);
        }
        public void AddPoints (int addPoints) {
            points += addPoints;
        }
        public void Render() {
            display.SetText(points.ToString());
            display.RenderText();
        }
        public int GetPoints() {
            return points;
        }
    }
}