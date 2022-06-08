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
        /// <summary> Adds points to the point field to be displayed later</summary>
	    /// <param name="addPoints">int that should be added to points.</param>
        public void AddPoints (int addPoints) {
            points += addPoints;
        }
        /// <summary> Sets the text to the points field and render it</summary>
        public void Render() {
            display.SetText(points.ToString());
            display.RenderText();
        }
        /// <summary> Get the points</summary>
        /// <return> Returns the points as an int</return>
        public int GetPoints() {
            return points;
        }
    }
}





