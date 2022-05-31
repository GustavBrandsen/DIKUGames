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
        
        public bool CheckTimer() {
            return Math.Round(StaticTimer.GetElapsedSeconds()) == gameTime;
        }
        
        public int GetElapsedSeconds() {
            return Convert.ToInt32(StaticTimer.GetElapsedSeconds());
        }
        
        public void Render() {
            timerText.SetText((gameTime-GetElapsedSeconds()).ToString());
            timerText.RenderText();
        }
        
        public void ResetTimer() {
            StaticTimer.RestartTimer();
        }
        
        public void PauseTimer() {
            StaticTimer.PauseTimer();
        }
        
        public void ResumeTimer() {
            StaticTimer.ResumeTimer();
        }
        
        public void AddTime(double extraTime) {
            gameTime += extraTime;
        }
    }
}