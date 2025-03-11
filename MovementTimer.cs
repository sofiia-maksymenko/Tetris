namespace Tetris;

public class MovementTimer
{
    private float _fallInterval;
    private float _fallTimer;

    public MovementTimer(float fallInterval)
    {
        _fallInterval = fallInterval;
    }
    
    public bool Update(float elapsedTimeInSeconds)
    {
        _fallTimer += elapsedTimeInSeconds;

        if (_fallTimer >= _fallInterval)
        {
            _fallTimer = 0;
            return true;
        }

        return false;
    }
}