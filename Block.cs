using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris;

public class Block
{
    private Texture2D _block;

    public Point Position { get; set; }

    public Block(Point startPosition, GraphicsDevice graphicsDevice)
    {
        Position = startPosition;

        _block = new Texture2D(
            graphicsDevice, 
            Constants.BlockSize, 
            Constants.BlockSize);

        Color[] data = new Color[Constants.BlockSize * Constants.BlockSize];
        for (int i = 0; i < data.Length; i++)
            data[i] = Color.White;
        _block.SetData(data);
    }

    public void Move(Point offset)
    {
        Position = new Point(Position.X + offset.X, Position.Y + offset.Y);
    }

    public void Draw(SpriteBatch spriteBatch, BlockPositionConverter positionConverter)
    {
        spriteBatch.Draw(
            _block,
            positionConverter.ToScreenPosition(Position),
            Color.White);
    }
}