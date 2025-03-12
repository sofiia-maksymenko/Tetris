using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Tile
    {
        private Block[] _blocks;
        private BlockPositionConverter _positionConverter;

        public Tile(Point startPosition, GraphicsDevice graphicsDevice, BlockPositionConverter positionConverter)
        {

            _blocks = new[]
            {
                new Block(new Point(startPosition.X, startPosition.Y), graphicsDevice, positionConverter),
                new Block(new Point(startPosition.X + 1, startPosition.Y), graphicsDevice, positionConverter),
                new Block(new Point(startPosition.X, startPosition.Y + 1), graphicsDevice, positionConverter),
                new Block(new Point(startPosition.X + 1, startPosition.Y + 1), graphicsDevice, positionConverter)
            };
        }

        public void Move(Point offset)
        {
            foreach (var block in _blocks)
            {
                block.Move(offset);
            }
        }

        public bool IsOnGround()
        {
            foreach (var block in _blocks)
            {
                if (block.Position.Y == Constants.FieldHeight - 1)
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in _blocks)
            {
                block.Draw(spriteBatch);
            }
        }
    }
}

