using Microsoft.Xna.Framework.Graphics;

namespace Tetris;

public interface IScreen
{
    void Update(float elapsedTimeSeconds);
    
    void Draw(SpriteBatch spriteBatch);
}