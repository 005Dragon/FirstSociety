using Code.Metrics;
using Code.PlanetEditorV2.Settings;
using Code.Utils;
using UnityEngine;

namespace Code.PlanetEditorV2.Filters
{
    public class StepGradientShapeFilter : SphereShapeFilterBase
    {
        public override bool Enabled => Settings.Enabled;
        
        public StepGradientSettings Settings;

        [ReadOnly]
        public float StepHeight;

        public override Vector3 Evaluate(Vector3 value, Range<float> magnitudeRange)
        {
            float magnitude = magnitudeRange.Max - magnitudeRange.Min;

            StepHeight = magnitude / Settings.StepsCount;

            float currentMagnitude = value.magnitude - magnitudeRange.Min;

            int stepMultiplier = (int)(currentMagnitude / StepHeight);

            if (currentMagnitude % StepHeight > 0.5f)
            {
                stepMultiplier++;
            }

            return value / value.magnitude * (stepMultiplier * StepHeight + magnitudeRange.Min);
        }
    }
}