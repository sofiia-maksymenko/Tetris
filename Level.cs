using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Tetris
{
    public class Level
    {
        private readonly Texture2D _borderTexture;
        private readonly BlockPositionConverter _positionConverter;
        private readonly List<Block> _placedBlocks = new List<Block>();
        private int score = 0;

        public Level(GraphicsDevice graphicsDevice, BlockPositionConverter positionConverter)
        {
            _borderTexture = new Texture2D(graphicsDevice, 1, 1);
            _borderTexture.SetData(new[] { Color.White });

            _positionConverter = positionConverter;
        }

        public bool IsOccupied(Point position)
        {
            foreach (var block in _placedBlocks)
            {
                if (block.Position == position)
                    return true;
            }
            return false;
        }

        public bool HasBlocksAboveField()
        {
            foreach (var block in _placedBlocks)
            {
                if (block.Position.Y < 0)
                {
                    return true;
                }
            }
            return false;
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
            spriteBatch.Draw(_borderTexture, new Rectangle((int)topLeft.X + width - thickness, (int)topLeft.Y, thickness, height), Color.White);
            spriteBatch.Draw(_borderTexture, new Rectangle((int)topLeft.X, (int)topLeft.Y + height - thickness, width, thickness), Color.White);

            foreach (var block in _placedBlocks)
            {
                block.Draw(spriteBatch, _positionConverter);
            }
        }

        public int Score => score;

        public void IntegrateTile(Tile tile)
        {
            _placedBlocks.AddRange(tile.Blocks);
            score += 10;
            ClearFullRows();
        }

        private void ClearFullRows()
        {
            int rowsCleared = 0;
            for (int y = Constants.FieldHeight - 1; y >= 0; y--) 
            {
                if (IsRowFull(y)) 
                {
                    RemoveRow(y); 
                    ShiftRowsDown(y);
                    rowsCleared++;
                    y++; 
                }
            }
            score += rowsCleared * 50;
        }

        private bool IsRowFull(int y)
        {
            int count = 0;
            foreach (var block in _placedBlocks)
            {
                if (block.Position.Y == y)
                    count++;

                if (count == Constants.FieldWidth)
                    return true;
            }
            return false;
        }

        private void RemoveRow(int y)
        {
            _placedBlocks.RemoveAll(b => b.Position.Y == y);
        }

        private void ShiftRowsDown(int removedRow)
        {
            foreach (var block in _placedBlocks)
            {
                if (block.Position.Y < removedRow)
                {
                    block.Position = new Point(block.Position.X, block.Position.Y + 1);
                }
            }
        } 
    }
}

