using System;
using Code.PlanetEditor.Filters;
using Code.PlanetEditorV2.Filters;
using UnityEditor;
using UnityEngine;

namespace Code.PlanetEditorV2.Settings
{
    [Serializable]
    public class SphereSettings : EditorSettingsBase
    {
        public string SphereName = "Sphere";

        public string SphereFaceName = "SphereFace";
        
        public Material Material;
        
        [Range(1, 7)]
        public int Resolution = 6;

        [Min(1)]
        public float Radius = 1;

        public bool SingleMesh;
    }
}