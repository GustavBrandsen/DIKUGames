using System;
namespace Breakout {
    /// <summary>
    /// GameStateType identifying the different states to control what to show on the screen
    /// </summary>
    public enum GameStateType
    {
        GameRunning, 
        GamePaused, 
        MainMenu,
        GameOver
    }
    public class StateTransformer {
        /// <summary> Transform string to enum type</summary>
	    /// <param name="state">String that should be transformed.</param>
        /// <return> Returns enum GameStateType</return>
        public static GameStateType TransformStringToState(string state) {
            switch (state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "MAIN_MENU":
                    return GameStateType.MainMenu;
                case "GAME_OVER":
                    return GameStateType.GameOver;
                default:
                    throw new ArgumentException("Invalid input");
            }
        }
    }
}