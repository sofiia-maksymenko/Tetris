using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Tile
    {
        public Block[] _blocks;

        public Tile(Point startPosition, GraphicsDevice graphicsDevice)
        {
            _blocks = new[]
            {
                new Block(new Point(startPosition.X, startPosition.Y), graphicsDevice),
                new Block(new Point(startPosition.X + 1, startPosition.Y), graphicsDevice),
                new Block(new Point(startPosition.X, startPosition.Y + 1), graphicsDevice),
                new Block(new Point(startPosition.X + 1, startPosition.Y + 1), graphicsDevice)
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

        public void Draw(SpriteBatch spriteBatch, BlockPositionConverter positionConverter)
        {
            foreach (var block in _blocks)
            {
                block.Draw(spriteBatch, positionConverter);
            }
        }

        public Block[] GetBlocks()
        {
            return _blocks;
        }
    }
}
