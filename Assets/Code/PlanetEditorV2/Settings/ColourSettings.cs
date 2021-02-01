using System;
using UnityEngine;

namespace Code.PlanetEditorV2.Settings
{
    [Serializable]
    public class ColourSettings : EditorSettingsBase
    {
        public Material Material;
        
        public Gradient Gradient;
    }
}
