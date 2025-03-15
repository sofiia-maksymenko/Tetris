using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public class Level
    {
        private Texture2D _borderTexture;
        private BlockPositionConverter _positionConverter;

        private List<Block> _placedBlocks = new List<Block>();

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

                if (IsOccupied(block.Position))
                {
                    return true;
                }

            }
            return false;
        }

        public bool IsOccupied(Point position)
        {
            return _placedBlocks.Any(block => block.Position == position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 topLeft = _positionConverter.ToScreenPosition(new Point(0, 0));
            Vector2 bottomRight = _positionConverter.ToScreenPosition(new Point(Constants.FieldWidth, Constants.FieldHeight));
            int width = (int)(bottomRight.X - topLeft.X);
            int height = (int)(bottomRight.Y - topLeft.Y);

            const int thickness = 3;

            spriteBatch.Draw(_borderTexture, new Rectangle((int)topLeft.X, (int)topLeft.Y, width, thickness), Color.White);
            spriteBatch.Draw(_borderTexture, new Rectangle((int)topLeft.X, (int)topLeft.Y, thickness, height), Color.White);
            spriteBatch.Draw(_borderTexture, new Rectangle((int)topLeft.X  + width - thickness, (int)topLeft.Y, thickness, height), Color.White);
            spriteBatch.Draw(_borderTexture, new Rectangle((int)topLeft.X, (int)topLeft.Y + height - thickness, width, thickness), Color.White);

            foreach (var block in _placedBlocks)
            {
                block.Draw(spriteBatch, _positionConverter);
            }
        }

        public void IntegrateTile(Tile tile)
        {
            _placedBlocks.AddRange(tile.GetBlocks());
        }
    }
}
