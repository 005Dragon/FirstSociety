using Code.PlanetEditor.Settings;
using UnityEngine;

namespace Code.PlanetEditor.Filters
{
    public class SimpleNoiseFilter
    {
        public NoiseSettings Settings { get; }
        
        private readonly Utils.Noise _noise = new Utils.Noise();

        public SimpleNoiseFilter(NoiseSettings settings)
        {
            Settings = settings;
        }

        public float Evaluate(float currentEvaluation, Vector3 point)
        {
            float totalNoiseValue = 0;
            float frequency = Settings.BaseRoughness;
            float amplitude = 1;

            for (int i = 0; i < Settings.LayersCount; i++)
            {
                float layerNoiseValue = _noise.Evaluate(point * frequency + Settings.Centre);
                
                totalNoiseValue += (layerNoiseValue + 1) * 0.5f * amplitude;

                frequency *= Settings.Roughness;
                amplitude *= Settings.Presistence;
            }

            Settings.Evaluate = currentEvaluation + totalNoiseValue;
            
            return currentEvaluation + totalNoiseValue * Settings.Strength.Evaluate(currentEvaluation + totalNoiseValue);
        }
    }
}
