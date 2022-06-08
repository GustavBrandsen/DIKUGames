using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using System;

namespace Breakout {
    public class GameTime {
        private StaticTimer staticTimer = default!;
        private double gameTime = default!;
        private Text timerText = default!;
        public GameTime (int seconds) {
            gameTime = (double)seconds;
            timerText = new Text("", new Vec2F(0.45f, -0.3f), new Vec2F(0.4f, 0.4f));
            timerText.SetColor(System.Drawing.Color.White);
            staticTimer = new StaticTimer();
        }
        
        /// <summary> Check if the StaticTimer is equal to the given gameTime</summary>
        /// <return> Returns a bool</return>
        public bool CheckTimer() {
            return Math.Round(StaticTimer.GetElapsedSeconds()) == gameTime;
        }
        
        /// <summary> Returns the elapsed seconds</summary>
        /// <return> Returns an int</return>
        public int GetElapsedSeconds() {
            return Convert.ToInt32(StaticTimer.GetElapsedSeconds());
        }
        /// <summary> Set the timerText to the correct number and render the text to the screen </summary>
        public void Render() {
            timerText.SetText((gameTime-GetElapsedSeconds()).ToString());
            timerText.RenderText();
        }
        /// <summary> Reset the StaticTimer </summary>
        public void ResetTimer() {
            StaticTimer.RestartTimer();
        }
        
        /// <summary> Pause the StaticTimer </summary>
        public void PauseTimer() {
            StaticTimer.PauseTimer();
        }
        
        /// <summary> Resume the StaticTimer </summary>
        public void ResumeTimer() {
            StaticTimer.ResumeTimer();
        }
        
        /// <summary> Adds time to the countdown</summary>
	    /// <param name="extraTime">A double.</param>
        public void AddTime(double extraTime) {
            gameTime += extraTime;
        }
    }
}