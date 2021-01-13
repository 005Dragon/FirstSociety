using UnityEngine;

namespace Code.PlanetEditor.Settings
{
    [CreateAssetMenu]
    public class ShapeSettings : ScriptableObject
    {
        public float Radius = 1;

        public NoiseLayerSettings[] NoiseLayers;
    }
}
