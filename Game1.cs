using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    public class Game1 : Game
    {
        private BlockPositionConverter _positionConverter;
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ScoreDisplay _scoreDisplay;
        private readonly Random _random = Random.Shared;
        private KeyboardHandler _keyboardHandler;
        private MovementTimer _movementTimer;
        private Tile _currentTile;
        private Tile _nextTile;
        private Level _level;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _positionConverter = new BlockPositionConverter(Constants.ScreenWidth, Constants.ScreenHeight);
            _level = new Level(GraphicsDevice, _positionConverter);
            _movementTimer = new MovementTimer(Constants.FallOneStepDurationSeconds);
            _keyboardHandler = new KeyboardHandler();
            _scoreDisplay = new ScoreDisplay(Content.Load<SpriteFont>("Arial"), _level);

            _nextTile = GenerateRandomTile();
            GenerateNextTile();
        }

        protected override void Update(GameTime gameTime)
        {

            var elapsedTimeInSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _keyboardHandler.Update();

            HandleInput();

            if (_movementTimer.Update(elapsedTimeInSeconds))
            {
                if (_currentTile.CanMoveDown(_level))
                {
                    _currentTile.Move(new Point(0, 1));
                }
                else
                {
                    _level.IntegrateTile(_currentTile);
    
                    if (IsGameOver())
                    {
                        Exit();
                    }
                    else
                    {
                        GenerateNextTile();    
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _scoreDisplay.Draw(_spriteBatch);
            _level.Draw(_spriteBatch);
            _currentTile.Draw(_spriteBatch, _positionConverter);
            _nextTile.Draw(_spriteBatch, _positionConverter);

            _spriteBatch.End();
        }

        private Tile GenerateRandomTile()
        {
            TileType randomType = (TileType)_random.Next(Enum.GetValues(typeof(TileType)).Length);
            return new Tile(new Point(5, 0), randomType, GraphicsDevice);
        }

        private void GenerateNextTile()
        {
            _currentTile = _nextTile;
            _currentTile.Position = new Point(Constants.FieldWidth / 2, 0);
            
            _nextTile = GenerateRandomTile();
            _nextTile.Position = new Point(12, 2);
        } 

        private bool IsGameOver()
        {
            return _level.HasBlocksAboveField() || _level.IsOccupied(new Point(5, 0));
        }

        private void HandleInput()
        {
            if (_keyboardHandler.IsPressedOnce(Keys.Left) && _currentTile.CanMove(new Point(-1, 0), _level))
            {
                _currentTile.Move(new Point(-1, 0));
            }

            if (_keyboardHandler.IsPressedOnce(Keys.Right) && _currentTile.CanMove(new Point(1, 0), _level))
            {
                _currentTile.Move(new Point(1, 0));
            }

            if (_keyboardHandler.IsPressedOnce(Keys.Down) && _currentTile.CanMove(new Point(0, 1), _level))
            {
                _currentTile.Move(new Point(0, 1));
            }

            if (_keyboardHandler.IsPressedOnce(Keys.Up))
            {
                _currentTile.Rotate(Rotation.Clockwise, _level);
            }

            if (_keyboardHandler.IsPressedOnce(Keys.Z))
            {
                _currentTile.Rotate(Rotation.CounterClockwise, _level);
            }

            if (_keyboardHandler.IsPressedOnce(Keys.Space))
            {
                while (_currentTile.CanMove(new Point(0, 1), _level))
                {
                    _currentTile.Move(new Point(0, 1));
                }
            }
        }
    }
}