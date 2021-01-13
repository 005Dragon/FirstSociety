using System.Collections.Generic;
using System.Linq;
using Code.PlanetEditor.Filters;
using Code.PlanetEditor.Settings;
using UnityEngine;

namespace Code.PlanetEditor
{
    public class ShapeCalculator
    {
        private readonly ShapeSettings _settings;
        private readonly NoiseLayerFilter[] _noiseLayers;

        public ShapeCalculator(ShapeSettings settings)
        {
            _settings = settings;
            _noiseLayers = _settings.NoiseLayers.Select(x => new NoiseLayerFilter(x)).ToArray();
        }

        public Vector3 CalculatePointOnSphere(Vector3 pointOnUnitSphere)
        {
            IEnumerable<NoiseLayerFilter> enabledNoiseLayers = _noiseLayers.Where(x => x.Enabled);

            float evaluation = 1;
            
            foreach (NoiseLayerFilter noiseLayer in enabledNoiseLayers)
            {
                evaluation = noiseLayer.Filter.Evaluate(evaluation, pointOnUnitSphere);
            }

            return pointOnUnitSphere * _settings.Radius * evaluation;
        }
    }
}
