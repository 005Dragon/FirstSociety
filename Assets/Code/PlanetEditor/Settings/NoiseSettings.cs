using Code.Utils;
using UnityEditor.UIElements;
using UnityEngine;

namespace Code.PlanetEditor.Settings
{
    [System.Serializable]
    public class NoiseSettings
    {

        public AnimationCurve Strength;
        public float BaseRoughness = 1;
        public float Roughness = 2;
        public float Presistence = 0.5f;
        
        public Vector3 Centre;

        [Range(1, 8)]
        public int LayersCount = 1;

        [ReadOnly]
        public float Evaluate;
    }
}
