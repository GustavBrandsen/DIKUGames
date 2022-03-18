using DIKUArcade.Math;
using DIKUArcade.Graphics;
namespace Galaga {
    public class Score {
        private int score;
        private Text display;
        public Score (Vec2F position, Vec2F extent) {
            score = 0;
            display = new Text (score.ToString(), position, extent);
            display.SetColor(System.Drawing.Color.White);
        }
        public void AddPoints () {
            score++;
        }
        public void RenderScore() {
            display.SetText(score.ToString());
            display.RenderText();
        }
    }
}