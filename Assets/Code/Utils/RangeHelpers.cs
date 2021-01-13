using UnityEngine;

namespace Code.Utils
{
    public static class RangeHelpers
    {
        public static void ToRange(ref float value, float? minValue = null, float? maxValue = null)
        {
            if (value < minValue)
            {
                value = (float)minValue;
            }

            if (value > maxValue)
            {
                value = (float)maxValue;
            }
        }
    }
}
