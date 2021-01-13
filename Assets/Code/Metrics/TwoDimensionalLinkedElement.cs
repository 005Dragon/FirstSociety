using System.Collections.Generic;
using Code.Utils;

namespace Code.Metrics
{
    public class TwoDimensionalLinkedElement<T>
    {
        public T Value { get; }

        public TwoDimensionalLinkedElement<T> this[Direction4 direction] => _area[direction];
        
        private readonly Dictionary<Direction4, TwoDimensionalLinkedElement<T>> _area =
            new Dictionary<Direction4, TwoDimensionalLinkedElement<T>>
            {
                {Direction4.Right, null},
                {Direction4.Left, null},
                {Direction4.Up, null},
                {Direction4.Down, null}
            };

        public TwoDimensionalLinkedElement(T value)
        {
            Value = value;
        }

        public bool TryCreate(Direction4 direction, T value)
        {
            if (_area[direction] != null)
            {
                return false;
            }
            
            _area[direction] = new TwoDimensionalLinkedElement<T>(value);
            this[direction]._area[direction.Inverse()] = this;

            return true;
        }

        public bool TryLink(Direction4 direction4, TwoDimensionalLinkedElement<T> other)
        {
            if (this[direction4] != null)
            {
                return false;
            }

            if (other[direction4.Inverse()] != null)
            {
                return false;
            }

            _area[direction4] = other;
            other._area[direction4.Inverse()] = this;

            return true;
        }
    }
}