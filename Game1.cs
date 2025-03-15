using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    public class Game1 : Game
    {
        private BlockPositionConverter _positionConverter;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Random _random = new Random();
        private KeyboardState _previousKeyboardState;
        private MovementTimer _movementTimer;
        private Tile _tile;
        private Level _level;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _positionConverter = new BlockPositionConverter(Constants.ScreenWidth, Constants.ScreenHeight);
            _level = new Level(GraphicsDevice, _positionConverter);
            _movementTimer = new MovementTimer(Constants.FallOneStepDurationSeconds);

            GenerateNewTile();
        }

        protected override void Update(GameTime gameTime)
        {
            var elapsedTimeInSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState state = Keyboard.GetState();
            

            HandleInput(state);

            if (_movementTimer.Update(elapsedTimeInSeconds))
            {
                if (_tile.CanMoveDown(_level))
                {
                    _tile.Move(new Point(0, 1));
                }
                else
                {
                    _level.IntegrateTile(_tile);
                    GenerateNewTile();
                }
            }

            _previousKeyboardState = state;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            _level.Draw(spriteBatch);
            _tile.Draw(spriteBatch, _positionConverter);

            spriteBatch.End();
        }

        private void GenerateNewTile()
        {
            TileType randomType = (TileType)_random.Next(Enum.GetValues(typeof(TileType)).Length);
            _tile = new Tile(new Point(5, 0), randomType, GraphicsDevice);

        }

        private void HandleInput(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Left) && _previousKeyboardState.IsKeyUp(Keys.Left) && _tile.CanMove(new Point(-1, 0), _level))
            {
                _tile.Move(new Point(-1, 0));
            }

            if (state.IsKeyDown(Keys.Right) && _previousKeyboardState.IsKeyUp(Keys.Right) && _tile.CanMove(new Point(1, 0), _level))
            {
                _tile.Move(new Point(1, 0));
            }

            if (state.IsKeyDown(Keys.Down) && _previousKeyboardState.IsKeyUp(Keys.Down) && _tile.CanMove(new Point(0, 1), _level))
            {
                _tile.Move(new Point(0, 1));
            }

            _previousKeyboardState = state;
        }
    }
}
