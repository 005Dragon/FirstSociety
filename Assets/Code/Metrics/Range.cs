using System;
using UnityEngine;

namespace Code.Metrics
{
    [Serializable]
    public struct Range<TValue>
        where TValue : IComparable<TValue>
    {
        public TValue Min;

        public TValue Max;

        public Range(TValue min, TValue max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Проверяет входит ли значение в указанный диапозон.
        /// </summary>
        public bool Contains(TValue value)
        {
            if (value.CompareTo(Min) < 0)
            {
                return false;
            }

            if (value.CompareTo(Max) > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Расширяет дапозон до указанного значения. 
        /// </summary>
        public void Expand(TValue value)
        {
            if (Min.CompareTo(value) > 0)
            {
                Min = value;
            }
            
            if (Max.CompareTo(value) < 0)
            {
                Max = value;
            }
        }

        public void Expand(Range<TValue> other)
        {
            if (Min.CompareTo(other.Min) > 0)
            {
                Min = other.Min;
            }

            if (Max.CompareTo(other.Max) < 0)
            {
                Max = other.Max;
            }
        }
    }
}