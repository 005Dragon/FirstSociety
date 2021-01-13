using System;
using UnityEngine;

namespace Code.PlanetEditorV2.Settings
{
    [Serializable]
    public class NoiseShapeSettings : EditorSettingsBase
    {
        public bool Enabled;
        public AnimationCurve LastMagnitudeMaskStrength = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
        public AnimationCurve Strength;
        public float BaseRoughness = 1;
        public float Roughness = 2;
        public float Presistence = 0.5f;
        
        public Vector3 Centre;

        [Range(1, 8)]
        public int LayersCount = 1;
    }
}