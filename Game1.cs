using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Game1 : Game
    {
        private const int FieldWidth = 10;
        private const int FieldHeight = 20;
        private const int BlockSize = 20;

        // TODO: calculate draw offset based on screen size so that the field is centered.

        //private Vector2 FieldDrawOffset = new Vector2(0, 0);

        private Vector2 FieldDrawOffset;

        private const int blockWidth = 2;
        private const int blockHeight = 2;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D block;

        private Point _blockPosition = new Point(0, 0);

        private double fallTimer = 0;
        private const double fallInterval = 0.5;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            block = new Texture2D(GraphicsDevice, BlockSize, BlockSize);

            Color[] data = new Color[BlockSize * BlockSize];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.White;
            block.SetData(data);


            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            FieldDrawOffset = new Vector2(
                (screenWidth - FieldWidth * BlockSize) / 2,
                (screenHeight - FieldHeight * BlockSize) / 2

            );
        }

        
        protected override void Update(GameTime gameTime)
        {
            //TODO: uncomment and test what happens. how do we improve this?

            fallTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (fallTimer >= fallInterval)
            {
                fallTimer = 0;

                if (_blockPosition.Y < FieldHeight - 1)
                {
                    _blockPosition.Y += 1;
                }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            for (int x = 0; x < blockWidth; x++)
            {
                for (int y = 0; y < blockHeight; y++)
                {
                    spriteBatch.Draw(
                        block,
                        FieldToWorldPosition(_blockPosition + new Point(x, y)),
                        Color.White);
                }
            }

            spriteBatch.End();
        }

        private Vector2 FieldToWorldPosition(Point blockPosition)
        {
            //TODO Sofiia: Implement proper function from constants, to calculate a world
            // position to render a rectangle on the screen in the right position using the
            // first four constants in the class.
            return new Vector2(

                FieldDrawOffset.X + blockPosition.X * BlockSize,
                FieldDrawOffset.Y + blockPosition.Y * BlockSize

            );
        }
    }
}