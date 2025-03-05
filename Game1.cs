using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D block;
        int blockSize = 20;

        int y = 0;
        int ground;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            ground = graphics.PreferredBackBufferHeight - blockSize;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            block = new Texture2D(GraphicsDevice, blockSize, blockSize);

            Color[] data = new Color[blockSize * blockSize];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.White;
            block.SetData(data);
        }

        protected override void Update(GameTime gameTime)
        {
            if (y < ground)
                y += 2;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(block, new Vector2(100, 0), Color.White);
            spriteBatch.End();
        }
    }
}
