using System;
using Code.PlanetEditorV2.Filters;
using Code.PlanetEditorV2.Settings;
using UnityEngine;

namespace Code.PlanetEditorV2.Generators
{
    #if UNITY_EDITOR
    [Serializable]
    public class PlanetWaterGenerator : GeneratorBase<PlanetWaterSettings>
    {
        public GameObject Planet;

        public PlanetGroundGenerator PlanetGroundGenerator;

        [HideInInspector]
        public GameObject Water;

        private readonly SphereGenerator _sphereGenerator = new SphereGenerator();

        public void Initialize(
            GameObject planet, 
            PlanetGroundGenerator planetGroundGenerator,
            Action<GameObject> destroyImmediate)
        {
            Planet = planet;
            
            DestroyImmediate = destroyImmediate;
            
            PlanetGroundGenerator = planetGroundGenerator;
            
            _sphereGenerator.Initialize(DestroyImmediate);
        }

        public float GetWaterLevelAltitude()
        {
            float stepHeight = PlanetGroundGenerator.StepGradientShapeFilter.StepHeight;
            
            return PlanetGroundGenerator.Altitude.Min + stepHeight * (Settings.StepIndex + Settings.Offset);
        }

        protected override GameObject GenerateCore()
        {
            _sphereGenerator.Settings = ConstructSphereSettings();
            Water = _sphereGenerator.Generate();
            Water.transform.parent = Planet.transform;

            return Water;
        }

        protected override bool ValidateCore()
        {
            if (Planet == null)
            {
                return false;
            }

            if (PlanetGroundGenerator == null)
            {
                return false;
            }

            return true;
        }

        private SphereSettings ConstructSphereSettings()
        {
            return new SphereSettings
            {
                Material = new Material(Settings.Material),
                Resolution = Settings.Resolution,
                SphereName = Settings.WaterName,
                SphereFaceName = Settings.WaterFaceName,
                SingleMesh = true,
                Radius = GetWaterLevelAltitude()
            };
        }
    }
    #endif
}