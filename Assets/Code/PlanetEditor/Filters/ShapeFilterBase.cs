using UnityEngine;

namespace Code.PlanetEditor.Filters
{
    public abstract class ShapeFilterBase : MonoBehaviour
    {
        public abstract Vector3 Evaluate(Vector3 value);
    }
}