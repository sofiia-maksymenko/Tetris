using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class KeyboardHandler
    {
        private KeyboardState _currentState;
        private KeyboardState _previousState;

        public void Update()
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState();
        }

        public bool IsPressedOnce(Keys key)
        {
            return _currentState.IsKeyDown(key) && _previousState.IsKeyUp(key);
        }

        public bool IsKeyDown(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }
    }
}
