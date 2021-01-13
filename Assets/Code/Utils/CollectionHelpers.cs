using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Code.Utils
{
    public static class CollectionHelpers
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> source)
        {
            if (source == null)
            {
                return true;
            }
            
            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return true;
                }
            }

            return false;
        }

        public static IEnumerable<T> ToSingleElementEnumerable<T>(this T value)
        {
            yield return value;
        }
    }
}