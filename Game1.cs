using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Game1 : Game
    {
        private const int FieldWidth = 10;
        private const int FieldHeight = 20;
        
        private Vector2 FieldDrawOffset;

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

            _blocks = new[]
            {
                new Block(new Point(0, 0), GraphicsDevice),
                new Block(new Point(1, 0), GraphicsDevice),
                new Block(new Point(0, 1), GraphicsDevice),
                new Block(new Point(1, 1), GraphicsDevice)
            };

            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            FieldDrawOffset = new Vector2(
                (screenWidth - FieldWidth * Constants.BlockSize) / 2,
                (screenHeight - FieldHeight * Constants.BlockSize) / 2

            );

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
                    if (block.Position.Y == FieldHeight - 1)
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
            return new Vector2(

                FieldDrawOffset.X + blockPosition.X * Constants.BlockSize,
                FieldDrawOffset.Y + blockPosition.Y * Constants.BlockSize
            );
        }
    }
}