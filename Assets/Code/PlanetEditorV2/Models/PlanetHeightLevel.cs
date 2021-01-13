using System;
using Code.Utils;

namespace Code.PlanetEditorV2.Models
{
    [Serializable]
    public class PlanetHeightLevel
    {
        [ReadOnly]
        public int Index;

        [ReadOnly]
        public float Altitude;
    }
}