﻿using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class KeyboardHandler
    {
        private KeyboardState _currentState;
        private KeyboardState _previousState;

        public KeyboardHandler()
        {
            _currentState = Keyboard.GetState();
            _previousState = _currentState;
        }

        public void Update()
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState();
        }

        public bool IsPressedOnce(Keys key)
        {
            return _currentState.IsKeyDown(key) && _previousState.IsKeyUp(key);
        }
    }
}
