using System;
using Code.Metrics;
using Code.PlanetEditor.Settings;
using Code.PlanetEditorV2.Settings;
using UnityEngine;

namespace Code.PlanetEditorV2.Filters
{
    public class NoiseShapeFilter : SphereShapeFilterBase
    {
        public override bool Enabled => Settings.Enabled;

        public NoiseShapeSettings Settings;
        
        private readonly Utils.Noise _noise = new Utils.Noise();

        public override Vector3 Evaluate(Vector3 point, Range<float> magnitude)
        {
            float totalNoiseValue = 0;
            float frequency = Settings.BaseRoughness * 0.01f;
            float amplitude = 1;

            for (int i = 0; i < Settings.LayersCount; i++)
            {
                float layerNoiseValue = _noise.Evaluate(point * frequency + Settings.Centre);
                
                totalNoiseValue += (layerNoiseValue + 1) * 0.5f * amplitude;

                frequency *= Settings.Roughness;
                amplitude *= Settings.Presistence;
            }

            totalNoiseValue *= Settings.Strength.Evaluate(totalNoiseValue);
            
            float pointUnitMagnitude = (point.magnitude - magnitude.Min) / (magnitude.Max - magnitude.Min);

            totalNoiseValue *= Settings.LastMagnitudeMaskStrength.Evaluate(pointUnitMagnitude);

            return point + point * totalNoiseValue;
        }
    }
}
