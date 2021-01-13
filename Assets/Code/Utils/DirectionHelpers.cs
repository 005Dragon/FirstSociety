using System;
using Code.Metrics;

namespace Code.Utils
{
    public static class DirectionHelpers
    {
        public static Direction4 Inverse(this Direction4 direction)
        {
            switch (direction)
            {
                case Direction4.Left: return Direction4.Right;
                case Direction4.Right: return Direction4.Left;
                case Direction4.Up: return Direction4.Down;
                case Direction4.Down: return Direction4.Up;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}