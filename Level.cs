using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Level
    {
        private Texture2D _borderTexture;
        private BlockPositionConverter _positionConverter;

        public Level(GraphicsDevice graphicsDevice, BlockPositionConverter positionConverter)
        {
            _borderTexture = new Texture2D(graphicsDevice, 1, 1);
            _borderTexture.SetData(new[] { Color.White });

            _positionConverter = positionConverter;
        }

        public bool IsColliding(Tile tile)
        {
            foreach (var block in tile.GetBlocks())
            {
                if (block.Position.X < 0 || block.Position.X >= Constants.FieldWidth ||
                    block.Position.Y >= Constants.FieldHeight)
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 screenSize = _positionConverter.ToScreenPosition(new Point(Constants.FieldWidth, Constants.FieldHeight));
            int width = (int)screenSize.X;
            int height = (int)screenSize.Y;

            int thickness = 3;

            spriteBatch.Draw(_borderTexture, new Rectangle(0, 0, width, thickness), Color.White);
            spriteBatch.Draw(_borderTexture, new Rectangle(0, 0, thickness, height), Color.White);
            spriteBatch.Draw(_borderTexture, new Rectangle(width - thickness, 0, thickness, height), Color.White);
            spriteBatch.Draw(_borderTexture, new Rectangle(0, height - thickness, width, thickness), Color.White);
        }
    }
}
