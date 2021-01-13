using System;
using Code.PlanetEditorV2.Filters;
using UnityEngine;

namespace Code.PlanetEditorV2.Settings
{
    [Serializable]
    public class PlanetGroundSettings : EditorSettingsBase
    {
        public SphereSettings SphereSettings;

        public NoiseShapeSettings LowLevelNoiseSettings;

        public NoiseShapeSettings MiddleLevelNoiseSettings;

        public NoiseShapeSettings HighLevelNoiseSettings;

        public StepGradientSettings StepGradientSettings;
        
        public ColourSettings ColourSettings;
    }
}