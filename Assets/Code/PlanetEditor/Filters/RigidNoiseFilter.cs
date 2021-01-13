using Code.PlanetEditor.Settings;
using UnityEngine;

namespace Code.PlanetEditor.Filters
{
    // public class RigidNoiseFilter : IReliefFilter<NoiseSettings>
    // {
    //     private readonly Utils.Noise _noise = new Utils.Noise();
    //     private readonly NoiseSettings _settings;
    //
    //     public RigidNoiseFilter(NoiseSettings settings)
    //     {
    //         _settings = settings;
    //     }
    //
    //     public float Evaluate(float currentEvaluation, Vector3 point)
    //     {
    //         float totalNoiseValue = (_noise.Evaluate(point * _settings.Roughness + _settings.Centre) + 1) * 0.5f;
    //         float frequency = _settings.BaseRoughness;
    //         float amplitude = 1;
    //
    //         for (int i = 0; i < _settings.LayersCount; i++)
    //         {
    //             float layerNoiseValue = _noise.Evaluate(point * frequency + _settings.Centre);
    //             
    //             totalNoiseValue += (layerNoiseValue + 1) * 0.5f * amplitude;
    //
    //             frequency *= _settings.Roughness;
    //             amplitude *= _settings.Presistence;
    //         }
    //
    //         return currentEvaluation + totalNoiseValue * _settings.Strength;
    //     }
    // }
}