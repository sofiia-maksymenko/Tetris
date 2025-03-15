using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tetris
{
    public class Game1 : Game
    {
        private BlockPositionConverter _positionConverter;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
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
            _tile = new Tile(new Point(5, 0), TileType.J, GraphicsDevice);
            //TileType randomType = (TileType)(new Random().Next(Enum.GetValues(typeof(TileType)).Length));
            //_tile = new Tile(new Point(5, 0), randomType, graphicsDevice);
        }
    }
}
