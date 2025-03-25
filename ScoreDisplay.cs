using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class ScoreDisplay
    {
        private readonly SpriteFont _font;
        private readonly Level _level;

        public ScoreDisplay(SpriteFont font, Level level)
        {
            _font = font;
            _level = level;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, $"Punkte: {_level.Score}", new Vector2(10, 10), Color.White);
        }
    }
}
