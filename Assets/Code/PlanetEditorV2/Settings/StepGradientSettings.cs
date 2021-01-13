using System;
using Code.Metrics;
using UnityEngine;

namespace Code.PlanetEditorV2.Settings
{
    [Serializable]
    public class StepGradientSettings : EditorSettingsBase
    {
        public bool Enabled;
        
        [Range(3, 32)]
        public int StepsCount;
    }
}