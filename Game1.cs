using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

            _tile = new Tile(new Point(1, 3), GraphicsDevice);

            _level = new Level(GraphicsDevice, _positionConverter);

            _movementTimer = new MovementTimer(Constants.FallOneStepDurationSeconds);
        }

        protected override void Update(GameTime gameTime)
        {
            var elapsedTimeInSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_movementTimer.Update(elapsedTimeInSeconds))
            {
                if (!_level.IsColliding(_tile) && !_tile.IsOnGround())
                {
                    _tile.Move(new Point(0, 1));
                }
                else
                {
                    _tile = new Tile(new Point(5, 0), GraphicsDevice); 
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
    }
}
