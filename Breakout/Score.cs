using DIKUArcade.Math;
using DIKUArcade.Graphics;
namespace Breakout {
    public class Score {
        private int score;
        private Text display;
        public Score (Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text (score.ToString(), position, extent);
            display.SetColor(System.Drawing.Color.White);
        }
        public void AddPoints (int addPoints) {
            score += addPoints;
        }
        public void Render() {
            display.SetText(score.ToString());
            display.RenderText();
        }
        public int GetScore() {
            return score;
        }
    }
}