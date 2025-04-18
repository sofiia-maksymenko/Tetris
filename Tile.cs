﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Tile
    {
        private readonly Block[] _blocks;
        private Point _position;

        public Tile(Point startPosition, TileType tileType, GraphicsDevice graphicsDevice)
        {
            _position = startPosition;
            switch (tileType)
            {
                case TileType.O:
                    _blocks = new[]
                    {
                        new Block(new Point(startPosition.X, startPosition.Y), graphicsDevice) { Color = Color.Yellow },
                        new Block(new Point(startPosition.X + 1, startPosition.Y), graphicsDevice) { Color = Color.Yellow },
                        new Block(new Point(startPosition.X, startPosition.Y + 1), graphicsDevice) { Color = Color.Yellow },
                        new Block(new Point(startPosition.X + 1, startPosition.Y + 1), graphicsDevice) { Color = Color.Yellow }
                    };
                    break;

                case TileType.I:
                    _blocks = new[]
                    {
                        new Block(new Point(startPosition.X, startPosition.Y), graphicsDevice) { Color = Color.Cyan },
                        new Block(new Point(startPosition.X, startPosition.Y + 1), graphicsDevice) { Color = Color.Cyan },
                        new Block(new Point(startPosition.X, startPosition.Y + 2), graphicsDevice) { Color = Color.Cyan },
                        new Block(new Point(startPosition.X, startPosition.Y + 3), graphicsDevice) { Color = Color.Cyan }
                    };
                    break;

                case TileType.L:
                    _blocks = new[]
                    {
                        new Block(new Point(startPosition.X, startPosition.Y), graphicsDevice) { Color = Color.Orange },
                        new Block(new Point(startPosition.X, startPosition.Y + 1), graphicsDevice) { Color = Color.Orange },
                        new Block(new Point(startPosition.X, startPosition.Y + 2), graphicsDevice) { Color = Color.Orange },
                        new Block(new Point(startPosition.X + 1, startPosition.Y + 2), graphicsDevice) { Color = Color.Orange }
                    };
                    break;

                case TileType.J:
                    _blocks = new[]
                    {
                        new Block(new Point(startPosition.X + 1, startPosition.Y), graphicsDevice) { Color = Color.Blue },
                        new Block(new Point(startPosition.X + 1, startPosition.Y + 1), graphicsDevice) { Color = Color.Blue },
                        new Block(new Point(startPosition.X + 1, startPosition.Y + 2), graphicsDevice) { Color = Color.Blue },
                        new Block(new Point(startPosition.X, startPosition.Y + 2), graphicsDevice) { Color = Color.Blue }
                    };
                    break;

                case TileType.T:
                    _blocks = new[]
                    {
                        new Block(new Point(startPosition.X, startPosition.Y), graphicsDevice) { Color = Color.Purple },
                        new Block(new Point(startPosition.X - 1, startPosition.Y + 1), graphicsDevice) { Color = Color.Purple },
                        new Block(new Point(startPosition.X, startPosition.Y + 1), graphicsDevice) { Color = Color.Purple },
                        new Block(new Point(startPosition.X + 1, startPosition.Y + 1), graphicsDevice) { Color = Color.Purple }
                    };
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null);
            }
        }

        public Point Position
        {
            get => _position;
            set
            {
                var oldPosition = _position;
                var newPosition = value;

                var difference = newPosition - oldPosition;
                foreach (var block in _blocks)
                {
                    block.Position += difference;
                }

                _position = value;
            }
        }


        public void Move(Point offset)
        {
            foreach (var block in _blocks)
            {
                block.Move(offset);
            }
        }

        public bool CanMoveDown(Level level)
        {
            foreach (var block in _blocks)
            {
                Point newPosition = new Point(block.Position.X, block.Position.Y + 1);


                if (newPosition.Y >= Constants.FieldHeight)
                {
                    return false;
                }


                if (level.IsOccupied(newPosition))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanMove(Point offset, Level level)
        {
            foreach (var block in _blocks)
            {
                Point newPosition = new Point(block.Position.X + offset.X, block.Position.Y + offset.Y);


                if (newPosition.X < 0 || newPosition.X >= Constants.FieldWidth || newPosition.Y >= Constants.FieldHeight)
                {
                    return false;
                }


                if (level.IsOccupied(newPosition))
                {
                    return false;
                }
            }
            return true;
        }

        public void Draw(SpriteBatch spriteBatch, BlockPositionConverter positionConverter)
        {
            foreach (var block in _blocks)
            {
                block.Draw(spriteBatch, positionConverter);
            }
        }

        public Block[] Blocks => _blocks;

        public void Rotate(Rotation direction, Level level)
        {
            if (_blocks.Length == 0) return;

            Point center = _blocks[0].Position;

            Point[] oldPositions = new Point[_blocks.Length];
            for (int i = 0; i < _blocks.Length; i++)
            {
                oldPositions[i] = _blocks[i].Position;
            }

            for (int i = 0; i < _blocks.Length; i++)
            {
                int relativeX = _blocks[i].Position.X - center.X;
                int relativeY = _blocks[i].Position.Y - center.Y;

                int newX, newY;

                switch (direction)
                {
                    case Rotation.Clockwise:
                        newX = -relativeY;
                        newY = relativeX;
                        break;
                    case Rotation.CounterClockwise:
                        newX = relativeY;
                        newY = -relativeX;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }

                _blocks[i].Position = new Point(center.X + newX, center.Y + newY);
            }


            if (!CanRotate(level))
            {

                for (int i = 0; i < _blocks.Length; i++)
                {
                    _blocks[i].Position = oldPositions[i];
                }
            }
        }

        private bool CanRotate(Level level)
        {
            foreach (var block in _blocks)
            {
                if (block.Position.X < 0 || block.Position.X >= Constants.FieldWidth || block.Position.Y >= Constants.FieldHeight)
                {
                    return false;
                }
                if (level.IsOccupied(block.Position))
                {
                    return false;
                }
            }
            return true;
        } 
    }
}


