using Microsoft.Xna.Framework;
namespace Tetris
{
    public class BlockPositionConverter
    {
        private Vector2 _fieldDrawOffset;

        public BlockPositionConverter(int screenWidth, int screenHeight)
        {
            _fieldDrawOffset = new Vector2(
                (screenWidth - Constants.FieldWidth * Constants.BlockSize) / 2,
                (screenHeight - Constants.FieldHeight * Constants.BlockSize) / 2
            );
        }
        public Vector2 ToScreenPosition(Point blockPosition)
        {
            return new Vector2(
                _fieldDrawOffset.X + blockPosition.X * Constants.BlockSize,
                _fieldDrawOffset.Y + blockPosition.Y * Constants.BlockSize
            );
        }
    }
}
