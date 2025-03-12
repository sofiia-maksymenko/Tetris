using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Game1 : Game
    {
        
        private BlockPositionConverter _positionConverter;


        private const int blockWidth = 2;
        private const int blockHeight = 2;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private MovementTimer _movementTimer;
        private Block[] _blocks;
        private Tile _tile;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            _positionConverter = new BlockPositionConverter(screenWidth, screenHeight);

            _tile = new Tile(new Point(1, 3), GraphicsDevice, _positionConverter);

            _movementTimer = new MovementTimer(0.7f);
        }

        protected override void Update(GameTime gameTime)
        {
            var elapsedTimeInSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_movementTimer.Update(elapsedTimeInSeconds))
            {
                if (!_tile.IsOnGround())
                {
                    _tile.Move(new Point(0, 1));
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            _tile.Draw(spriteBatch);

            spriteBatch.End();
        }

    }
}