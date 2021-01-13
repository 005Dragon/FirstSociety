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

        protected override GameObject GenerateCore()
        {
            _sphereGenerator.Settings = ConvertToSphereSettings(Settings);
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

        private SphereSettings ConvertToSphereSettings(PlanetWaterSettings planetWaterSettings)
        {
            float stepHeight = PlanetGroundGenerator.StepGradientShapeFilter.StepHeight;
            
            float radius = PlanetGroundGenerator.Altitude.Min + stepHeight * (planetWaterSettings.StepIndex + planetWaterSettings.Offset);

            return new SphereSettings
            {
                Material = planetWaterSettings.Material,
                Resolution = planetWaterSettings.Resolution,
                SphereName = planetWaterSettings.WaterName,
                SphereFaceName = planetWaterSettings.WaterFaceName,
                SingleMesh = true,
                Radius = radius
            };
        }
    }
    #endif
}