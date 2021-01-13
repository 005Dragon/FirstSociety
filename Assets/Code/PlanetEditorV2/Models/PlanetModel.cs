using Code.Metrics;
using Code.Utils;
using UnityEngine;

namespace Code.PlanetEditorV2.Models
{
    public class PlanetModel : MonoBehaviour
    {
        [ReadOnly] 
        public Range<float> Altitude;

        [ReadOnly]
        public PlanetHeightLevel WaterLevel;
        
        [ReadOnly]
        public PlanetHeightLevel[] GroundLevels;
    }
}