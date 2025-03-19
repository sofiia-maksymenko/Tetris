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
        private SpriteFont font;
        private Random _random = new Random();
        private KeyboardHandler _keyboardHandler;
        private MovementTimer _movementTimer;
        private Tile _tile;
        private Level _level;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
            _keyboardHandler = new KeyboardHandler();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Arial");
            _positionConverter = new BlockPositionConverter(Constants.ScreenWidth, Constants.ScreenHeight);
            _level = new Level(GraphicsDevice, _positionConverter);
            _movementTimer = new MovementTimer(Constants.FallOneStepDurationSeconds);

            GenerateNewTile();
        }

        protected override void Update(GameTime gameTime)
        {
            _keyboardHandler.Update();

            var elapsedTimeInSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            HandleInput();

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

                    if (IsGameOver())
                    {
                        Exit();
                        return;
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Punkte: " + _level.GetScore(), new Vector2(10, 10), Color.White);

            _level.Draw(spriteBatch);
            _tile.Draw(spriteBatch, _positionConverter);

            spriteBatch.End();
        }

        private void GenerateNewTile()
        {
            TileType randomType = (TileType)_random.Next(Enum.GetValues(typeof(TileType)).Length);
            _tile = new Tile(new Point(5, 0), randomType, GraphicsDevice);
        }

        private bool IsGameOver()
        {
            return _level.HasBlocksAboveField() || _level.IsOccupied(new Point(5, 0)); 
        }

        private void HandleInput()
        {
            if (_keyboardHandler.IsPressedOnce(Keys.Left) && _tile.CanMove(new Point(-1, 0), _level))
            {
                _tile.Move(new Point(-1, 0));
            }

            if (_keyboardHandler.IsPressedOnce(Keys.Right) && _tile.CanMove(new Point(1, 0), _level))
            {
                _tile.Move(new Point(1, 0));
            }

            if (_keyboardHandler.IsPressedOnce(Keys.Down) && _tile.CanMove(new Point(0, 1), _level))
            {
                _tile.Move(new Point(0, 1));
            }

            if (_keyboardHandler.IsPressedOnce(Keys.Up))
            {
                _tile.Rotate(clockwise: true, _level);
            }

            if (_keyboardHandler.IsPressedOnce(Keys.Z))
            {
                _tile.Rotate(clockwise: false, _level);
            }
        }
    }
}
