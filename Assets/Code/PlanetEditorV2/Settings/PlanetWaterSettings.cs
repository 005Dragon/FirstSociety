using System;
using Code.PlanetEditorV2.Filters;
using Code.PlanetEditorV2.Generators;
using UnityEngine;

namespace Code.PlanetEditorV2.Settings
{
    [Serializable]
    public class PlanetWaterSettings : EditorSettingsBase
    {
        public string WaterName = "Water";

        public string WaterFaceName = "WaterFace";
        
        public Material Material;
        
        [Range(1, 5)]
        public int Resolution = 5;
        
        [Range(0, 28)]
        public int StepIndex;
        
        [Range(-0.99f, 0.99f)]
        public float Offset = 0.5f;
    }
}