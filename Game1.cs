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

            _blocks = new[]
            {
                new Block(new Point(0, 0), GraphicsDevice,_positionConverter),
                new Block(new Point(1, 0), GraphicsDevice, _positionConverter),
                new Block(new Point(0, 1), GraphicsDevice, _positionConverter),
                new Block(new Point(1, 1), GraphicsDevice, _positionConverter)
            };

            _movementTimer = new MovementTimer(0.7f);
        }

        protected override void Update(GameTime gameTime)
        {
            var elapsedTimeInSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_movementTimer.Update(elapsedTimeInSeconds))
            {
                var isOnGround = false;
                foreach (var block in _blocks)
                {
                    if (block.Position.Y == Constants.FieldHeight - 1)
                    {
                        isOnGround = true;
                        break;
                    }
                }

                if (!isOnGround)
                {
                    foreach (var block in _blocks)
                    {
                        block.Move(new Point(0, 1));
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            foreach (var block in _blocks)
            {
                block.Draw(spriteBatch, this);
            }

            spriteBatch.End();
        }

        public Vector2 FieldToWorldPosition(Point blockPosition)
        {
            return _positionConverter.ToScreenPosition(blockPosition);
        }
    }
}