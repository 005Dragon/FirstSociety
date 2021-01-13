using System;
using Code.Metrics;
using UnityEngine;

namespace Code.Benchmark
{
    public class EndBenchmarkTrigger : MonoBehaviour
    {
        public PlanetaryCoordinates targetCoordinates;

        public Range<float> LatitudeRangeTriggerZone;

        public Range<float> LongtitudeRangeTriggerZone;
        
        public bool EndBenchmark;

        private void Update()
        {
            bool latitudeTriggered = LatitudeRangeTriggerZone.Contains(targetCoordinates.Position.Latitude);
            bool longtitudeTriggered = LongtitudeRangeTriggerZone.Contains(targetCoordinates.Position.Longitude);

            if (latitudeTriggered && longtitudeTriggered)
            {
                EndBenchmark = true;
            }
        }
    }
}