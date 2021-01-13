using Code.Utils;
using UnityEngine;

namespace Code.Metrics
{
    [System.Serializable]
    public struct PlanetaryPosition
    {
        [ReadOnly]
        public float Latitude;
        
        [ReadOnly]
        public float Longitude;
        
        [ReadOnly]
        public float Altitude;

        public PlanetaryPosition(float latitude, float longitude, float altitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }
    }
}
